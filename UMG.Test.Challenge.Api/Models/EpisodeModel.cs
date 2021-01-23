using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Api.Models
{
    public class EpisodeModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public string DeliverPartnerName { get; set; }
        public Boolean Released { get; set; }
    }
}
