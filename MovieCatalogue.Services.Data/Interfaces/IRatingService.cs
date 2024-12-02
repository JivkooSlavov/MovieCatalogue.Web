using MovieCatalogue.Web.ViewModels.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Services.Data.Interfaces
{
    public interface IRatingService
    {
        Task AddOrUpdateRatingAsync(Guid movieId, Guid userId, int ratingValue);
        Task UpdateMovieRatingAsync(Guid movieId);
    }
}
