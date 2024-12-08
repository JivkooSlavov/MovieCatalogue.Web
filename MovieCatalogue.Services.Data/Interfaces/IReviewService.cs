﻿using MovieCatalogue.Web.ViewModels.Review;
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
        Task<ReviewEditViewModel> GetReviewForEditAsync(Guid id, Guid userId, bool isAdmin);
        Task<bool> UpdateReviewAsync(ReviewEditViewModel reviewVm, Guid userId, bool isAdmin);
        Task<ReviewDeleteViewModel> GetReviewForDeleteAsync(Guid id, Guid userId, bool isAdmin);
        Task<bool> DeleteReviewAsync(Guid reviewId, Guid userId, bool isAdmin);
        Task<IEnumerable<UserReviewViewModel>> GetUserReviewsByPageAsync(Guid userId, int pageNumber, int pageSize);
        Task<IEnumerable<ReviewViewModel>> GetAllReviewsByPageAsync(int page, int pageSize);
        Task<int> GetTotalReviewsForUserAsync(Guid userId);
        Task<int> GetTotalReviewsAsync();
    }
}
