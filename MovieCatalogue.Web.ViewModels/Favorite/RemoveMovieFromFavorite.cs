using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Web.ViewModels.Favorite
{
    public class RemoveMovieFromFavorite
    {
        public Guid MovieId { get; set; }
        public string MovieTitle { get; set; } = null!;
    }
}
