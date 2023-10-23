using MovingApi.Models;

namespace MovingApi.DTO
{
    public class MovieDitailsDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public double Rate { get; set; }
     
        public string Storeline { get; set; } = null!;

        public byte[] Poster { get; set; }
        public byte GenreID { get; set; }
        public string GenreName { get; set; }

    }
}
