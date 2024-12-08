using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCatalogue.Services.Data.Interfaces;
using MovieCatalogue.Web.Controllers;
using MovieCatalogue.Web.ViewModels.Review;
using System.Drawing.Printing;
using System.Security.Claims;
using static MovieCatalogue.Common.ApplicationConstants;
using static MovieCatalogue.Common.EntityValidationConstants.ReviewConstants;

namespace MovieCatalogue.Web.Areas.Admin.Controllers
{
    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]
    public class ReviewManagementController : BaseController
    {
        private readonly IMovieService _movieService;
        private readonly IReviewService _reviewService;
        public ReviewManagementController(IMovieService movieService, IReviewService reviewService)
        {
            _movieService = movieService;
            _reviewService = reviewService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var totalReviews = await _reviewService.GetTotalReviewsAsync();
            var totalPages = (int)Math.Ceiling(totalReviews / (double)PageSizeOfReviews);
            var reviews = await _reviewService.GetAllReviewsByPageAsync(page, PageSizeOfReviews);

            var model = new ReviewsListViewModel
            {
                Reviews = reviews,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            bool isAdmin = User.IsInRole(AdminRoleName);
            var review = await _reviewService.GetReviewForEditAsync(id, Guid.Empty, isAdmin);

            if (review == null)
            {
                return NotFound("Review not found or unauthorized.");
            }

            return View(review);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ReviewEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            bool isAdmin = User.IsInRole(AdminRoleName);
            var isUpdated = await _reviewService.UpdateReviewAsync(model, Guid.Empty, isAdmin);

            if (!isUpdated)
            {
                return NotFound("Error while updating the review.");
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool isAdmin = User.IsInRole(AdminRoleName);
            var review = await _reviewService.GetReviewForDeleteAsync(id, Guid.Empty, isAdmin);

            if (review == null)
            {
                return NotFound("Review not found.");
            }

            return View(review);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(ReviewDeleteViewModel model)
        {
            bool isAdmin = User.IsInRole(AdminRoleName);
            var isDeleted = await _reviewService.DeleteReviewAsync(model.Id, Guid.Empty, isAdmin);
            if (!isDeleted)
            {
                return NotFound("Error while deleting the review.");
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
