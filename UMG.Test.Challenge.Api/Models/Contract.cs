using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie.Api.Helper;

namespace Movie.Api.Models
{
    public class Contract
    {
        public int ContractId { get; set; }
        public int ArtistId { get; set; }
        public int PartnerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ContractTypeEnum ContractType { get; set; }
    }
}
