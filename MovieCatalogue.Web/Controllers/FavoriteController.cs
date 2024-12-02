using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data;
using MovieCatalogue.Data.Models;
using System.Security.Claims;
using MovieCatalogue.Web.ViewModels;
using MovieCatalogue.Web.ViewModels.Favorite;
using MovieCatalogue.Web.ViewModels.Movie;
using Microsoft.AspNetCore.Authorization;
using MovieCatalogue.Services.Data.Interfaces;

namespace MovieCatalogue.Web.Controllers
{
    [Authorize]
    public class FavoriteController : BaseController
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(MovieDbContext context, IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Guid currentUserId = Guid.Parse(GetUserId());

            var favorites = await _favoriteService.GetUserFavoritesAsync(currentUserId);

            return View(favorites);
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavorites(Guid movieId)
        {
            Guid movieGuid = Guid.Empty;
            bool isGuidValid = this.IsGuidValid(movieId.ToString(), ref movieGuid);
            if (!isGuidValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            Guid userId = Guid.Parse(GetUserId());

            var isAdded = await _favoriteService.AddToFavoritesAsync(movieId, userId);

            if (!isAdded)
            {
                return BadRequest("The movie is already in your favorites.");
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmRemove(Guid movieId)
        {
            Guid movieGuid = Guid.Empty;
            bool isGuidValid = this.IsGuidValid(movieId.ToString(), ref movieGuid);
            if (!isGuidValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            Guid userId = Guid.Parse(GetUserId());

            var model = await _favoriteService.GetFavoriteByMovieIdAsync(movieId, userId);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Remove(Guid movieId)
        {
            Guid movieGuid = Guid.Empty;
            bool isGuidValid = this.IsGuidValid(movieId.ToString(), ref movieGuid);
            if (!isGuidValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            Guid userId = Guid.Parse(GetUserId());

            var isRemoved = await _favoriteService.RemoveFavoriteAsync(movieId, userId);

            if (!isRemoved)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}