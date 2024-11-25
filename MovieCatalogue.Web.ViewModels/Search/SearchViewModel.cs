using MovieCatalogue.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Web.ViewModels.Search
{
    public class SearchViewModel
    {
        public string Query { get; set; } = string.Empty;
        public virtual ICollection<MovieSearchResultViewModel> Movies { get; set; } = new HashSet<MovieSearchResultViewModel>();
    }
}
