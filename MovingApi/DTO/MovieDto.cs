using MovingApi.Models;

namespace MovingApi.DTO
{
    public class MovieDto
    {
        [MaxLength(100)]
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public double Rate { get; set; }
        [MaxLength(2500)]
        public string Storeline { get; set; } = null!;

        public IFormFile Poster { get; set; } = null;
        public byte GenreID { get; set; }


    }
}
