using Microsoft.EntityFrameworkCore;
using MovingApi.Data;
using MovingApi.Models;

namespace MovingApi.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly ApplicationDbContext _context;

        public MovieServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> Create(Movie Movie)
        {
            _context.AddAsync(Movie);
            _context.SaveChanges();
            return Movie;
        }

        public Movie Delete(Movie Movie)
        {
            _context.Movies.Remove(Movie);
            _context.SaveChanges();
            return Movie;
        }

        public async Task<IEnumerable<Movie>> GetAll(byte genreId = 0)
        {
            return await _context.Movies
                     .Where(g => g.GenreID == genreId || genreId==0)
                     .Include(g => g.Genre)
                     .OrderByDescending(g => g.Rate)
                     .ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            return await _context.Movies.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);

        }

        public Movie Update(Movie Movie)
        {
            _context.Update(Movie);
            _context.SaveChanges();
            return Movie;
        }
    }
}
