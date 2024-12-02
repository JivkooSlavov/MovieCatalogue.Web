using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCatalogue.Services.Data.Interfaces;
using MovieCatalogue.Web.ViewModels.Rating;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieCatalogue.Web.Controllers
{
    [Authorize]
    public class RatingController : BaseController
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(RatingCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Movie", new { id = model.MovieId });
            }

            var userId = Guid.Parse(GetUserId());

            await _ratingService.AddOrUpdateRatingAsync(model.MovieId, userId, model.Value);

            return RedirectToAction("Details", "Movie", new { id = model.MovieId });
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
