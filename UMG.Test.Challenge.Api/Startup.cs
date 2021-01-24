using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Cosmonaut;
using Cosmonaut.Extensions.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Movie.Api.Data;
using Movie.Api.Data.Configuration;
using Movie.Api.Data.Configuration.MongoStore;
using Movie.Api.Data.Configuration.Store;
using Movie.Api.Interface;
using Movie.Api.Models;
using Movie.Api.Services;
using MongoDB.Driver;

namespace Movie.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var storeConfig = Configuration.GetSection("StoreConfiguration").Get<StoreSettings>();

            var cosmosSettings = new CosmosStoreSettings(storeConfig.Database,
                                                         storeConfig.AccountEndPoint,
                                                         storeConfig.AccountKey);

            services.AddCosmosStore<Artist>(cosmosSettings);
            //services.AddCosmosStore<Album>(cosmosSettings);

            var mongoStore = Configuration.GetSection("MongoDBStore").Get<MongoDBSettings>();

            var client = new MongoClient(mongoStore.ConnectionString);
            var mongoDatabase = client.GetDatabase(mongoStore.DatabaseName);
            services.AddSingleton(mongoDatabase);

            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Movie")));

            var autoMapping = new MapperConfiguration(am =>
            {
                am.AddProfile<AutoMapperProfile>();
            });

            var mapper = autoMapping.CreateMapper();

            RegisterService(services);

            //AddAuthentication(services);

            services.AddControllers(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                config.Filters.Add(new AuthorizeFilter(policy));
            }).AddNewtonsoftJson();

            //services.AddControllers()
            //    .AddNewtonsoftJson(options => options.UseMemberCasing());

            AddCors(services);
            AddCrossAccessOriginSharing(services);

            AddSwagger(services);
        }
        
        private void RegisterService(IServiceCollection services)
        {
            services.AddScoped<IArtistCollectionService, ArtistCollectionService>();
            services.AddScoped<IAlbumService, AlbumService>();
        }
        private void AddSwagger(IServiceCollection services)
        {
            services.AddOpenApiDocument(d =>
            {
                d.Title = Configuration.GetSection("Application").Value;
            });
        }
        private void AddCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("default",
                    builder => builder
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
            });
        }
        private void AddCrossAccessOriginSharing(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:4200",
                                        "http://localhost:8080/",
                                        "https://moviesstore.z13.web.core.windows.net/"
                                        );
                });
            });
        }

        private void AddAuthentication(IServiceCollection services)
        {
            var jwtSettings = new JwtSettings();
            Configuration.Bind(key: nameof(jwtSettings), jwtSettings);

            services.AddSingleton(jwtSettings);
            services
                .AddAuthentication(x =>
                {
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key: Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("default",
                    builder => builder
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //allowing default cors - cross origin resouce sharing
            app.UseCors("default"); 

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseReDoc();
        }
    }
}
