using Microsoft.AspNetCore.Mvc;
using MovingApi.Data;
using MovingApi.Models;
using MovingApi.Services;

namespace MovingApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresServices _services;

        public GenresController(IGenresServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await _services.GetAll();
            return Ok(genres);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(GenreDto dto)
        {
            var genre = new Genre { Name = dto.Name };
           await _services.Create(genre);
            return Ok(genre);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAsync(byte id, [FromBody] GenreDto dto)
        {
            var genre = await _services.GetById(id);
            if (genre is null) return NotFound($"No Genre was found With ID:{id}");

            _services.Update(genre);
            return Ok(genre);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var genre = await _services.GetById(id); 
            if (genre is null) return NotFound($"No Genre was found With ID:{id}");
            _services.Delete(genre);
            return Ok(genre);
        }
    }
}
