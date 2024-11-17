using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static MovieCatalogue.Common.EntityValidationConstants.MovieConstants;
using static MovieCatalogue.Common.EntityValidationConstants.RatingConstants;
using static MovieCatalogue.Common.Messages;

namespace MovieCatalogue.Web.ViewModels.Movie
{
    public class AddMovieViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(MovieTitleMaxLength, MinimumLength =MovieTitleMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(MovieDescriptionsMaxLength, MinimumLength = MovieDescriptionMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage =RequireErrorMessage)]
        public int GenreId { get; set; }

        [Required(ErrorMessage = RequireErrorMessage)]
        public string ReleaseDate { get; set; } = DateTime.Today.ToString(DateFormatOfMovie);

        [Required]
        [StringLength(MovieCastMaxLength, MinimumLength = MovieCastMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Cast { get; set; } = null!;

        [Required(ErrorMessage = RequireErrorMessage)]
        [Range(RatingValueMin, RatingValueMax)]
        public double Rating { get; set; }

        public string? TrailerUrl { get; set; } = null!;

        [Required]
        [StringLength(MovieImageUrlMaxLength, MinimumLength = MovieImageUrlMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string PosterUrl { get; set; } = null!;

        [Required]
        [StringLength(MovieDirectorMaxLength, MinimumLength = MovieDirectorMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Director { get; set; } = null!;

        [Required]
        [Range(MovieDurationMinLength,MovieDurationMaxLength, ErrorMessage = StringLengthErrorRange)]
        public int Duration { get; set; }
        public virtual IEnumerable<TypeOfGenreMovies> Genres { get; set; } = new List<TypeOfGenreMovies>();
    }
}
