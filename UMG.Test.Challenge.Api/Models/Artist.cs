using Cosmonaut.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Api.Models
{
    public class Artist
    {
        [CosmosPartitionKey]
        public string PartitionKey { get; set; }

        [JsonProperty("id")]
        public string ArtistId { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
    }
}
