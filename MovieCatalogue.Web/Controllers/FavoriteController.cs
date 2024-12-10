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
using static MovieCatalogue.Common.EntityValidationConstants.MovieConstants;

namespace MovieCatalogue.Web.Controllers
{
    [Authorize]
    public class FavoriteController : BaseController
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index(int page = 1)
        {
            Guid currentUserId = Guid.Parse(GetUserId());

            var totalFavorites = await _favoriteService.GetTotalFavoritesForUserAsync(currentUserId);
            var totalPages = (int)Math.Ceiling(totalFavorites / (double)PageSizeOfMovies);

            var favorites = await _favoriteService.GetUserFavoritesByPageAsync(currentUserId, page, PageSizeOfMovies);


            var model = new UserFavoritesListViewModel
            {
                Favorites = favorites,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(model);
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