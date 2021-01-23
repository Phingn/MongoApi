using Movie.Api.Models.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Api.Interface
{
    public interface IAlbumService
    {
        Task<List<Album>> GetAlbums();
        Task<Album> GetAlbumById(string albumId);
        Task<Album> CreateAlbum(Album album);
        Task<bool> UpdateAlbum(string albumId, Album album);
        Task<bool> RemoveAlbum(string albumId);
    }
}
