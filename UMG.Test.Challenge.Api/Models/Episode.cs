using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Api.Models
{
    public class Episode
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Artist Artist { get; set; }
        public DeliveryPartner DeliveryPartner { get; set; }
    }
}
