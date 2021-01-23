using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Movie.Api.Data;

namespace Movie.Api.Tests.Data
{
    public static class DBContext
    {
        public static DataContext DataSourceMemoryContext(string databaseInstance)
        {
            DbContextOptions<DataContext> options;
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseInMemoryDatabase(databaseName: databaseInstance);
            options = builder.Options;
            DataContext dataContext = new DataContext(options);
            //dataContext.Database.EnsureDeleted();
            //dataContext.Database.EnsureCreated();
            return dataContext;
        }
    }
}
