using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCatalogue.Services.Data.Interfaces;
using MovieCatalogue.Web.Controllers;
using MovieCatalogue.Web.ViewModels.Movie;
using System.Security.Claims;
using static MovieCatalogue.Common.ApplicationConstants;

namespace MovieCatalogue.Web.Areas.Admin.Controllers
{
    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]
    public class MovieManagementController : BaseController
    {
        private readonly IMovieService _movieService;

        public MovieManagementController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<MovieInfoViewModel> allMovies = await _movieService.GetAllMoviesAsync();
            return View(allMovies);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new AddMovieViewModel
            {
                Genres = await _movieService.GetGenresAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddMovieViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = await _movieService.GetGenresAsync();
                return View(model);
            }

            Guid adminId = Guid.Parse(GetUserId());
            bool result = await _movieService.AddMovieAsync(model, adminId);

            if (!result)
            {
                ModelState.AddModelError(nameof(model.ReleaseDate), "Invalid release date format.");
                model.Genres = await _movieService.GetGenresAsync();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            bool isAdmin = User.IsInRole(AdminRoleName);
            var model = await _movieService.GetMovieForEditAsync(id, Guid.Empty, isAdmin);

            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddMovieViewModel model, Guid id)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = await _movieService.GetGenresAsync();
                return View(model);
            }


            Guid currentUserId = Guid.Parse(GetUserId());
            bool isAdmin = User.IsInRole(AdminRoleName);

            bool isEdited = await _movieService.EditMovieAsync(id, model, currentUserId, isAdmin);

            if (!isEdited)
            {
                ModelState.AddModelError(nameof(model.ReleaseDate), "Invalid release date format.");
                model.Genres = await _movieService.GetGenresAsync();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            Guid currentUserId = Guid.Parse(GetUserId());
            bool isAdmin = User.IsInRole("Admin");

            var model = await _movieService.GetMovieForDeletionAsync(id, currentUserId, isAdmin);

            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            // Вземаме текущия потребител
            Guid currentUserId = Guid.Parse(GetUserId());
            bool isAdmin = User.IsInRole("Admin");

            // Изтриваме филма
            bool isDeleted = await _movieService.DeleteMovieAsync(id, currentUserId, isAdmin);

            if (!isDeleted)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
