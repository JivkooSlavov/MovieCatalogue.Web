using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Web.ViewModels.Movie
{
    public class DeleteMovieViewModel
    {
        public Guid Id { get; set; }

        public Guid MovieId { get; set; }
        public string Title { get; set; } = null!;
        public string GenreName { get; set; } = null!;
        public string PosterUrl { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public double Rating { get; set; }

        public Guid CreatedByUserId {  get; set; }
    }
}
