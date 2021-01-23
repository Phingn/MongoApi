using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie.Api.Interface;
using Movie.Api.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Movie.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistCollectionService _artist;

        public ArtistController(IArtistCollectionService artist)
        {
            _artist = artist;
        }

        [AllowAnonymous]
        [HttpGet]
        public Task<List<Artist>>Get()
        {
            return _artist.GetAllArtists();
        }

        [HttpGet("{id}")]
        public Task<Artist> Get(string id)
        {
            return _artist.GetArtist(id);
        }

        [HttpPost("artist")]
        public Task<string> Post([FromBody] Artist artist)
        {
            return _artist.CreateArtist(artist);
        }

        [HttpPut("update/{id}")]
        public Task<bool> Put(string id, [FromBody] Artist artist)
        {
            return _artist.UpdateArtist(artist, id);
        }

        [HttpDelete("{id}")]
        public Task<bool> Delete(string id)
        {
            return _artist.DeleteArtist(id);
        }
    }
}
