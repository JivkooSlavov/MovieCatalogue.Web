using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Services.Data.Interfaces;
using MovieCatalogue.Web.ViewModels;
using MovieCatalogue.Web.ViewModels.Movie;
using MovieCatalogue.Web.ViewModels.Review;
using System.Security.Claims;

namespace MovieCatalogue.Web.Controllers
{
    public class ReviewController : BaseController
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(Guid movieId)
        {
            Guid movieGuid = Guid.Empty;
            bool isGuidValid = this.IsGuidValid(movieId.ToString(), ref movieGuid);
            if (!isGuidValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            var model = await _reviewService.GetReviewsForMovieAsync(movieId);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UserReviews()
        {
            var userId = Guid.Parse(GetUserId());
            var reviews = await _reviewService.GetUserReviewsAsync(userId);

            return View(reviews);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create(Guid movieId)
        {
            Guid movieGuid = Guid.Empty;
            bool isGuidValid = this.IsGuidValid(movieId.ToString(), ref movieGuid);
            if (!isGuidValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            var reviewVm = new ReviewCreateViewModel
            {
                MovieId = movieId
            };
            return View(reviewVm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ReviewCreateViewModel reviewVm)
        {
            if (!ModelState.IsValid)
            {
                return View(reviewVm);
            }

            var userId = Guid.Parse(GetUserId());
            var result = await _reviewService.CreateReviewAsync(reviewVm, userId);
            if (!result)
            {
                return BadRequest("Error while creating review");
            }
            return RedirectToAction("Details", "Movie", new { id = reviewVm.MovieId });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id)
        {
            Guid movieGuid = Guid.Empty;
            bool isGuidValid = this.IsGuidValid(id.ToString(), ref movieGuid);
            if (!isGuidValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            var userId = Guid.Parse(GetUserId());
            var reviewVm = await _reviewService.GetReviewForEditAsync(id, userId);

            if (reviewVm == null)
            {
                return NotFound();
            }

            return View(reviewVm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(ReviewEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _reviewService.UpdateReviewAsync(model);

            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction("Index", new { movieId = model.MovieId });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {

            Guid movieGuid = Guid.Empty;
            bool isGuidValid = this.IsGuidValid(id.ToString(), ref movieGuid);
            if (!isGuidValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            var userId = Guid.Parse(GetUserId());
            ReviewDeleteViewModel review = await _reviewService.GetReviewForDeleteAsync(id, userId);

            if (review == null)
            {
                return NotFound();
            }

            return this.View(review);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteConfirm(ReviewDeleteViewModel review)
        {
            var userId = Guid.Parse(GetUserId());
            var isDeleted = await _reviewService.DeleteReviewAsync(review.Id, userId);

            if (!isDeleted)
            {
                return NotFound();
            }
            return RedirectToAction("Index", new { movieId = review.MovieId });
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
