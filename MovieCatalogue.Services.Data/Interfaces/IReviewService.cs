using MovieCatalogue.Web.ViewModels.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Services.Data.Interfaces
{
    public interface IReviewService
    {
        Task<ReviewIndexViewModel> GetReviewsForMovieAsync(Guid movieId);
        Task<bool> CreateReviewAsync(ReviewCreateViewModel reviewVm, Guid userId);
        Task<ReviewCreateViewModel> GetReviewForEditAsync(Guid id, Guid userId);
        Task<bool> UpdateReviewAsync(ReviewEditViewModel reviewVm);
        Task<ReviewDeleteViewModel> GetReviewForDeleteAsync(Guid id, Guid userId);
        Task<bool> DeleteReviewAsync(Guid reviewId, Guid userId);
    }
}
