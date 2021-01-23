using Movie.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Api.Interface
{
    public interface IArtistCollectionService
    {
        Task<Artist> GetArtist(string partitionKey);
        Task<string> CreateArtist(Artist artist);
        Task<bool> UpdateArtist(Artist artist, string partitionKey);
        Task<bool> DeleteArtist(string partitionKey);
        Task<bool> UpdateStatus(bool status);
        Task<List<Artist>> GetAllArtists();
    }
}
