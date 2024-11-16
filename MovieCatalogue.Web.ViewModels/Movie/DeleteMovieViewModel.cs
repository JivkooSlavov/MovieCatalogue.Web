using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Web.ViewModels.Movie
{
    public class DeleteMovieViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string GenreName { get; set; } = null!;
        public string PosterUrl { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public double Rating { get; set; }

    }
}
