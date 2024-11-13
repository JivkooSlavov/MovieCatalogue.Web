namespace MovieCatalogue.Data.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime DatePosted { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}