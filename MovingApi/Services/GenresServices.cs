using Microsoft.EntityFrameworkCore;
using MovingApi.Data;
using MovingApi.Models;

namespace MovingApi.Services
{
    public class GenresServices : IGenresServices
    {
        private readonly ApplicationDbContext _context;

        public GenresServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Genre> Create(Genre genre)
        {
            await _context.AddAsync(genre);

            _context.SaveChanges();
            return genre;
        }

        public Genre Delete(Genre genre)
        {
            _context.Genres.Remove(genre);
            _context.SaveChanges();
            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await _context.Genres.OrderBy(g => g.Name).ToListAsync();

        }

        public async Task<Genre> GetById(byte id)
        {
            return await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<bool> ISValidGenre(byte id)
        {
           return await _context.Genres.AnyAsync(g => g.Id == id);
        }

        public Genre Update(Genre genre)
        {
            _context.Genres.Update(genre);
            _context.SaveChanges();
            return genre;
        }
    }
}
