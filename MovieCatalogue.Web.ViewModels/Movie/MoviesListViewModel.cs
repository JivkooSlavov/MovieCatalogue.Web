using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Web.ViewModels.Movie
{
    public class MoviesListViewModel
    {
        public IEnumerable<MovieInfoViewModel> Movies { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
