using MovieCatalogue.Web.ViewModels.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Web.ViewModels.Favorite
{
    public class UserFavoritesListViewModel
    {
        public IEnumerable<AddMovieToFavorite> Favorites { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }

}
