namespace MovingApi.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; } = null!; 
        public int Year { get; set; }
        public double Rate { get; set; }
        [MaxLength(2500)]
        public string Storeline { get; set; }= null!;

        public byte[] Poster { get; set; }
        public byte GenreID { get; set; }
        public Genre Genre { get; set; }

    }
}
