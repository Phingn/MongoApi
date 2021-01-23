using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie.Api.Models;
using Movie.Api.Models.dto;

namespace Movie.Api.Interface
{
    public interface IContractService
    {
        List<DistributorAlbum> GetAlbums(string partnerName, DateTime StartDate, DateTime EndDate);
        List<Artist> GetArtist();
    }
}
