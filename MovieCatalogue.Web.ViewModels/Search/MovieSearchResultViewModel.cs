using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Web.ViewModels.Search
{
    public class MovieSearchResultViewModel
    {
        public Guid Id { get; set; } 
        public string? Title { get; set; } 
        public string? Genre { get; set; } 
        public string? PosterUrl { get; set; }

        public string? Director {  get; set; }
    }
}
