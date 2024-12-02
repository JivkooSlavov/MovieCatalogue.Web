using MovieCatalogue.Web.ViewModels.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Web.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public IEnumerable<MovieInfoViewModel> PopularMovies { get; set; } = new List<MovieInfoViewModel>();
    }
}
