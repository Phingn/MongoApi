using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie.Api.Helper;

namespace Movie.Api.Models.dto
{
    public class DistributorAlbum
    {
        public Contract Contracts { get; set; }
        public string PartnerName { get; set; }
        public string AlbumName { get; set; }
        public string  ArtistName { get; set; }
    }
}
