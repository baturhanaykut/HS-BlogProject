using HS_BlogProject.Application.Models.DTOs.GenreDTOs;
using HS_BlogProject.Application.Services.GenreService;
using Microsoft.AspNetCore.Mvc;

namespace HS_BlogProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllGenre() 
        {
            var genre = await _genreService.GetGenres();

            if (genre is not null)
            {
                return Ok(genre);
            }
            else
                return BadRequest();
        
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetGenre(int id)
        {
            var genre = await _genreService.GetById(id);
            if (genre is not null)
            {
                return Ok(genre);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddGenre([FromBody] CreateGenreDTO genre)
        {
            await _genreService.Create(genre);

            //return CreatedAtAction("GetGenreName", new {Name = genre.Name},genre);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateGenre([FromBody] UpdateGenreDTO genre)
        {
            await _genreService.Update(genre);
            return Ok("Genre Güncellendi");
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            await _genreService.Delete(id);
            return Ok("Genre Silindi");
        }
    }
}
