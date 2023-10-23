using MovingApi.Models;

namespace MovingApi.Services
{
    public interface IGenresServices
    {

        Task<IEnumerable<Genre>> GetAll();


        Task<Genre> GetById(byte id);
        Task<Genre> Create(Genre genre);
        Genre Update(Genre genre);
        Genre Delete(Genre genre);
        Task<bool> ISValidGenre(byte id);
    }
}
