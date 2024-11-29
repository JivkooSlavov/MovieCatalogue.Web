namespace MovieCatalogue.Data.Models
{
    public class Review
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Content { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
        public Guid UserId { get; set; } 
        public User User { get; set; } = null!;
        public DateTime DatePosted { get; set; }
        public DateTime? UpdatePosted { get; set; }
    }
}