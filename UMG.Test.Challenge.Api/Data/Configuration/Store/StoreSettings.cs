using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Api.Data.Configuration.Store
{
    public class StoreSettings
    {
        public string Database { get; set; }
        public string AccountKey { get; set; }
        public string AccountEndPoint { get; set; }
    }
}
