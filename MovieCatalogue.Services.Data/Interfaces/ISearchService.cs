using MovieCatalogue.Web.ViewModels.Search;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Services.Data.Interfaces
{
    public interface ISearchService
    {
        Task<IEnumerable<MovieSearchResultViewModel>> SearchMoviesAsync(string query);
    }
}
