using Microsoft.AspNetCore.Identity;

namespace MovieCatalogue.Data.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<Favorite> Favorites { get; set; } = new HashSet<Favorite>();
        public virtual ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }
}