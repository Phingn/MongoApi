using Cosmonaut;
using Cosmonaut.Extensions;
using Movie.Api.Data.Configuration;
using Movie.Api.Interface;
using Movie.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Api.Services
{
    public class ArtistCollectionService : IArtistCollectionService
    {
        private readonly ICosmosStore<Artist> Store;
        public ArtistCollectionService(ICosmosStore<Artist> store)
        {
            Store = store;
        }
        public async Task<string> CreateArtist(Artist artist)
        {
            var artistId = Guid.NewGuid().ToString("D");
            try
            {
                var art = new Artist
                {
                    ArtistId = Guid.NewGuid().ToString("D"),
                    //PartitionKey = PartitionKeyGenerator.Create("R", artistId, 1000),
                    Name = artist.Name,
                    Status = artist.Status
                };
                await Store.AddAsync(art);
                return art.ArtistId.ToString();
            }
            catch(Exception e)
            {
                throw new Exception(e.ToString());
            }

        }

        public async Task<bool> DeleteArtist(string partitionKey)
        {
            var artist  = await GetArtist(partitionKey);
            if (artist != null)
            {
                //Note deleting is required partitionKey
                await Store.RemoveByIdAsync(artist.ArtistId.ToString(), artist.PartitionKey);
                return true;
            }
            return false;
        }

        public async Task<List<Artist>> GetAllArtists()
        {
            return await Store.Query().ToListAsync();
        }
        public async Task<Artist> GetArtist(string partitionKey)
        {
            return await Store.Query().FirstOrDefaultAsync(a => a.PartitionKey == partitionKey);
        }

        public async Task<bool> UpdateArtist(Artist artist, string partitionKey)
        {
            var art = GetArtist(partitionKey);
            if (artist != null)
            {
                artist.PartitionKey = partitionKey;
                await Store.UpdateAsync(artist);
                return true;
            }
            return false;
        }

        public Task<bool> UpdateStatus(bool status)
        {
            throw new NotImplementedException();
        }
    }
}
