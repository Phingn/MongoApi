using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Movie.Api.Data.Configuration.MongoStore
{
    public class MongoStore
    {
        public IMongoDatabase Database { get; set; }
    }
}
