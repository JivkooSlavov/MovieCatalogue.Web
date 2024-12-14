namespace MovieCatalogue.Data.Models
{
    public class Favorite
    {
        public Guid Id { get; set; }
        public Guid MovieId { get; set; }
        public bool IsDeleted { get; set; }
        public Movie Movie { get; set; } = null!;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}