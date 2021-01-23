using Movie.Api.Data.Configuration.MongoStore;
using Movie.Api.Interface;
using Movie.Api.Models.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Movie.Api.Services
{
    public class AlbumService : IAlbumService
    {
        //private readonly MongoDBSettings _mongoDBSettings;
        //IMongoDatabase db;

        private readonly IMongoDatabase _mongoDatabase;
        IMongoCollection<Album> collection;


        //public AlbumService(MongoDBSettings mogoStoreSettings)
        //{
        //    _mongoDBSettings = mogoStoreSettings;
        //    var client = new MongoClient(@_mongoDBSettings.ConnectionString);
        //    db = client.GetDatabase(_mongoDBSettings.DatabaseName);
        //    var database  = client.GetDatabase(_mongoDBSettings.DatabaseName);
        //    collection = database.GetCollection<Album>("Albums");

        //}
        public AlbumService(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
            collection = _mongoDatabase.GetCollection<Album>("Albums");
        }

        public async Task<Album> CreateAlbum(Album album)
        {
            try
            {
                await collection.InsertOneAsync(album);
            }
            catch(Exception e)
            {
                throw new Exception(e.ToString());
            }
            return album;
        }

        public async Task<Album> GetAlbumById(string albumId)
        {
            return await collection.Find<Album>(album => album.Id == albumId).FirstAsync();
        }

        public async Task<List<Album>> GetAlbums()
        {
            return await collection.Find(album => true).ToListAsync();
        }

        public async Task<bool> RemoveAlbum(string albumId)
        {
            await collection.DeleteOneAsync(album => album.Id == albumId);
            return true;
        }

        public async Task<bool> UpdateAlbum(string albumId, Album album)
        {
            await collection.ReplaceOneAsync(album => album.Id == albumId, album);
            return true;
        }
    }
}
