using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie.Api.Models;
using Movie.Api.Models.dto;

namespace Movie.Api.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }

        public virtual DbSet<DistributorAlbum> DistributorAlbums { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<DeliveryPartner> DeliveryPartners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Artist>(eb =>
                {
                    eb.HasKey(k => k.ArtistId);
                    eb.ToTable("Artist", "dbo");
                });

            modelBuilder
                .Entity<Album>(eb =>
                {
                    eb.HasKey(k => k.AlbumId);
                    eb.ToTable("Album", "dbo");
                });

            modelBuilder
                .Entity<DeliveryPartner>(eb =>
                {
                    eb.HasKey(k => k.PartnerId);
                    eb.ToTable("DeliveryPartner", "dbo");
                });

            modelBuilder
                .Entity<Contract>(eb =>
                {
                    eb.HasKey(k => k.ContractId);
                    eb.ToTable("Contract", "dbo");
                });

            modelBuilder
            .Entity<DistributorAlbum>(eb =>
            {
                eb.HasNoKey();
                eb.ToTable("DistributorAlbum", "dbo");
            });
        }
    }
}
