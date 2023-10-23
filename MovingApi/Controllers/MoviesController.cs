using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovingApi.Data;
using MovingApi.Models;
using MovingApi.Services;

namespace MovingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieServices _Movieservices;
  private readonly IGenresServices _Genreservices;
  private readonly IMapper _mapper;
        public MoviesController(IMovieServices movieservices, IGenresServices genreservices, IMapper mapper)
        {
            _Movieservices = movieservices;
            _Genreservices = genreservices;
            _mapper = mapper;
        }





        private long _maxallowedpostersize = 5242880;
        private new List<string> _allowedExtentions = new List<string>
        {
            ".jpg",".png"
        };
    
        [HttpGet]
        public async Task<IActionResult> GetallAsync()
        {
            var movies = await _Movieservices.GetAll();
            var data = _mapper.Map<IEnumerable<MovieDitailsDto>>(movies);
            return Ok(data);


        }
        [HttpGet("GetByGenraId")]
        public async Task<IActionResult> GetByGenraIdAsync(byte genreId)
        {
            var movies = await _Movieservices.GetAll(genreId);
            var data = _mapper.Map<IEnumerable<MovieDitailsDto>>(movies);
            return Ok(data);

        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetMovieByID(int Id)
        {
            var movie = await _Movieservices.GetById(Id);
            if (movie is null)
                return NotFound();

            var dto = _mapper.Map<MovieDitailsDto>(movie);
            return Ok(dto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] MovieDto dto)
        {

            if (!_allowedExtentions.Contains(Path.GetExtension(dto.Poster.FileName))) return BadRequest("Alloed File is jpg , png");
            if (_maxallowedpostersize < dto.Poster.Length) return BadRequest("The Max Alloed File is 5MB");

            var isvalidgenre = await _Genreservices.ISValidGenre(dto.GenreID);
            if (!isvalidgenre)
                return BadRequest("Invalid genre Id");


            using var Datastream = new MemoryStream();
            await dto.Poster.CopyToAsync(Datastream);
            
            
            var movie = _mapper.Map<Movie>(dto);
            movie.Poster= Datastream.ToArray();


            await _Movieservices.Create(movie);
            return Ok(movie);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAsync(int Id, [FromForm] MovieDto dto)
        {


            var isvalidgenre = await _Genreservices.ISValidGenre(dto.GenreID);
            if (!isvalidgenre)
                return BadRequest("Invalid genre Id");

            var movie = await _Movieservices.GetById(Id);
            if (movie is null) return NotFound("no movie found");
           
            if (dto.Poster != null)
            {
                if (!_allowedExtentions.Contains(Path.GetExtension(dto.Poster.FileName))) return BadRequest("Alloed File is jpg , png");
                if (_maxallowedpostersize < dto.Poster.Length) return BadRequest("The Max Alloed File is 5MB");
                using var Datastream = new MemoryStream();
                await dto.Poster.CopyToAsync(Datastream);
                movie.Poster = Datastream.ToArray();
            }
            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.GenreID = dto.GenreID;
            movie.Rate = dto.Rate;
            movie.Storeline = dto.Storeline;

           _Movieservices.Update(movie);
            return Ok(movie);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteMovieAsync(int id)
        {
            var movie = await _Movieservices.GetById(id);
            if (movie is null) return NotFound("no movie found");
             _Movieservices.Delete(movie);
            return Ok(movie);
        }
    }
}
