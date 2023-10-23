using MovingApi.Models;

namespace MovingApi.Services
{
    public interface IMovieServices
    {
        Task<IEnumerable<Movie>> GetAll(byte genreId=0);
        Task<Movie> GetById(int id);
        Task<Movie> Create(Movie Movie);
        Movie Update(Movie Movie);
        Movie Delete(Movie Movie);  
    }
}
