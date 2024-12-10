using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCatalogue.Data;
using MovieCatalogue.Services.Data.Interfaces;
using MovieCatalogue.Web.ViewModels.Movie;
using System.Security.Claims;
using static MovieCatalogue.Common.ApplicationConstants;
using static MovieCatalogue.Common.EntityValidationConstants.MovieConstants;

namespace MovieCatalogue.Web.Controllers
{
    public class MovieController : BaseController
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1)
        {
            var totalMovies = await _movieService.GetTotalMoviesAsync();
            var totalPages = (int)Math.Ceiling(totalMovies / (double)PageSizeOfMovies);

            var movies = await _movieService.GetMoviesByPageAsync(page, PageSizeOfMovies);

            var model = new MoviesListViewModel
            {
                Movies = movies,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(model);
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
                ModelState.AddModelError(nameof(model.ReleaseDate), "Invalid release date format.");
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