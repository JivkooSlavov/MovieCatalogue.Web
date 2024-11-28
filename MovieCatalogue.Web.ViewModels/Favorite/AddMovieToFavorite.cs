using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Web.ViewModels.Favorite
{
    public class AddMovieToFavorite
    {
        public int FavoriteId { get; set; }
        public Guid MovieId { get; set; }
        public string Genre { get; set; } = null!;
        public string DirectorName { get; set; } = null!;
        public string MovieTitle { get; set; } = null!;
        public string MovieDescription { get; set; } = null!;
        public string PosterUrl { get; set; } = null!;
        public bool IsFavorite { get; set; }
    }
}
