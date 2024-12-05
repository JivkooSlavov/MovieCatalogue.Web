using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Common;
using MovieCatalogue.Data;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Data.Repository.Interfaces;
using MovieCatalogue.Services.Data;
using MovieCatalogue.Services.Data.Interfaces;
using MovieCatalogue.Web.ViewModels.Movie;
using MovieCatalogue.Web.ViewModels.Review;
using System.Globalization;
using System.IO;
using System.Security.Claims;
using static MovieCatalogue.Common.EntityValidationConstants;
using static MovieCatalogue.Common.EntityValidationConstants.MovieConstants;
using static MovieCatalogue.Common.ApplicationConstants;

namespace MovieCatalogue.Web.Controllers
{
    public class MovieController : BaseController
    {
        private readonly IMovieService _movieService;

        public MovieController(MovieDbContext context, IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {

            IEnumerable<MovieInfoViewModel> allMovies =
               await  _movieService.GetAllMoviesAsync();

            return View(allMovies);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            Guid movieGuid = Guid.Empty;
            bool isGuidValid = this.IsGuidValid(id.ToString(), ref movieGuid);
            if (!isGuidValid)
            {
                return this.RedirectToAction(nameof(Index));
            }


            MovieInfoViewModel? movie = await this._movieService
               .GetMovieDetailsAsync(movieGuid);

            if (movie == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(movie);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var model = new AddMovieViewModel
            {
                Genres = await _movieService.GetGenresAsync()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(AddMovieViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = await _movieService.GetGenresAsync();
                return View(model);
            }

            Guid userId = Guid.Parse(GetUserId());
            bool result = await _movieService.AddMovieAsync(model, userId);

            if (!result)
            {
                this.ModelState.AddModelError(nameof(model.ReleaseDate),
                    String.Format(DateFormatOfMovie));
                model.Genres = await _movieService.GetGenresAsync();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
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

            Guid currentUserId = Guid.Parse(GetUserId());
            bool isAdmin = User.IsInRole(AdminRoleName);

            AddMovieViewModel? model = await this._movieService
                            .GetMovieForEditAsync(movieGuid, currentUserId, isAdmin);

            if (model==null)
            {
                return RedirectToAction(nameof(Index));
            }

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(AddMovieViewModel model, Guid id)
        {
            Guid movieGuid = Guid.Empty;
            bool isGuidValid = this.IsGuidValid(id.ToString(), ref movieGuid);
            if (!isGuidValid)
            {
                return this.RedirectToAction(nameof(Index));
            }
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
                ModelState.AddModelError(nameof(model.ReleaseDate), "You do not have permission to edit this movie.");
                model.Genres = await _movieService.GetGenresAsync();
                return View(model);
            }
            return RedirectToAction(nameof(Details), new { id });
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

            Guid currentUserId = Guid.Parse(GetUserId());
            bool isAdmin = User.IsInRole(AdminRoleName);

            DeleteMovieViewModel? model = await _movieService.GetMovieForDeletionAsync(id, currentUserId, isAdmin);

            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(DeleteMovieViewModel model)
        {
            Guid currentUserId = Guid.Parse(GetUserId());
            bool isAdmin = User.IsInRole(AdminRoleName);
            bool isDeleted = await _movieService.DeleteMovieAsync(model.Id, currentUserId, isAdmin);

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