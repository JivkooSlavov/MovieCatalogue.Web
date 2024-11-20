using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MovieCatalogue.Data.Models
{
    public class Movie
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public string Cast { get; set; } = null!;
        public double Rating { get; set; }
        public string TrailerUrl { get; set; } = null!;
        public string Director { get; set; } = null!;
        public int Duration { get; set; }
        public bool IsDeleted { get; set; }
        public string PosterUrl { get; set; } = null!;

        public Guid CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; } = null!;

        public virtual ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();
        public virtual ICollection<Favorite> Favorites { get; set; } = new HashSet<Favorite>();
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

    }
}
