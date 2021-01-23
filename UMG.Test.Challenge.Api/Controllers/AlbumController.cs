using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie.Api.Data.Configuration.MongoStore;
using Movie.Api.Interface;
using Movie.Api.Models.Mongo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Movie.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        private readonly MongoDBSettings _mogoDBSetting;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }
        // GET: api/<AlbumController>
        [HttpGet]
        public Task<List<Album>> Get()
        {
            return _albumService.GetAlbums();
        }

        // GET api/<AlbumController>/5
        [HttpGet("{id}")]
        public Task<Album> Get(string id)
        {
            return _albumService.GetAlbumById(id);
        }

        // POST api/<AlbumController>
        [HttpPost]
        public Task<Album> Post([FromBody] Album album)
        {
            return _albumService.CreateAlbum(album);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="album"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public Task<bool> Put(string id, [FromBody] Album album)
        {
            return _albumService.UpdateAlbum(id, album);
        }

        // DELETE api/<AlbumController>/5
        [HttpDelete("{id}")]
        public Task<bool> Delete(string id)
        {
            return _albumService.RemoveAlbum(id);
        }
    }
}
