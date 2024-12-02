namespace MovieCatalogue.Data.Models
{
    public class Rating
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Value { get; set; }
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
        public Guid UserId { get; set; } 
        public User User { get; set; } = null!;
    }
}