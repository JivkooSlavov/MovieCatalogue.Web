using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Common;
using MovieCatalogue.Data;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Web.ViewModels.Movie;
using System.Globalization;
using System.IO;
using System.Security.Claims;
using static MovieCatalogue.Common.EntityValidationConstants;
using static MovieCatalogue.Common.EntityValidationConstants.MovieConstants;

namespace MovieCatalogue.Web.Controllers
{

    public class MovieController : Controller
    {
        private readonly MovieDbContext _context;

        public MovieController(MovieDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {

            var model = await _context.Movies
                  .Where(x => x.IsDeleted == false)
                  .Include(y => y.Genre)
                .Select(x => new MovieInfoViewModel
                {
                    Id = x.Id,
                    Cast = x.Cast,
                    Description = x.Description,
                    Director = x.Director,
                    Duration = x.Duration,
                    PosterUrl = x.PosterUrl,
                    Genre = x.Genre.Name,
                    Rating = x.Rating,
                    ReleaseDate = x.ReleaseDate.ToString(DateFormatOfMovie),
                    Title = x.Title,
                    TrailerUrl = x.TrailerUrl,
                })
                .AsNoTracking()
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string? id)
        {

            var movie = await _context.Movies
                .Include(e=>e.Ratings)
                .Where(e=>e.Id.ToString() ==id)
                .Where(e => e.IsDeleted == false)
                .Select(e => new MovieInfoViewModel()
                {
                    Id = e.Id,
                    Cast = e.Cast,
                    Description = e.Description,
                    Director = e.Director,
                    Duration = e.Duration,
                    PosterUrl = e.PosterUrl,
                    Genre = e.Genre.Name,
                    Rating = e.Rating,
                    ReleaseDate = e.ReleaseDate.ToString(DateFormatOfMovie),
                    Title = e.Title,
                    TrailerUrl = e.TrailerUrl,
                    Ratings=e.Ratings.ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (movie == null)
            {
                return RedirectToAction(nameof(Index));
            }


            return View(movie);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            AddMovieViewModel model = new AddMovieViewModel();
            model.Genres = GetTypes();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(AddMovieViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = GetTypes();
                return View(model);
            }

            if (!GetTypes().Any(e => e.Id == model.GenreId))
            {
                ModelState.AddModelError(nameof(model.GenreId), "Genre does not exist!");
            }

            DateTime start;

            if (!DateTime.TryParseExact(
               model.ReleaseDate,
               DateFormatOfMovie,
               CultureInfo.InvariantCulture,
               DateTimeStyles.None,
               out start))
            {

                ModelState
                    .AddModelError(nameof(model.ReleaseDate), $"Invalid date! Format must be: {DateFormatOfMovie}");

                model.Genres = GetTypes();

                return View(model);
            }

            Movie movieToAdd = new Movie()
            {
               Title = model.Title,
               Description = model.Description,
               Cast = model.Cast,
               Director = model.Director,
               Duration = model.Duration,
               Rating = model.Rating,
               ReleaseDate = start,
               PosterUrl = model.PosterUrl,
               TrailerUrl = model.TrailerUrl,
               GenreId = model.GenreId
            };

            await _context.Movies.AddAsync(movieToAdd);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id)
        {
            Movie? existMovie = await _context.Movies
                .FindAsync(id);

            if (existMovie == null)
            {
                return BadRequest();
            }

            AddMovieViewModel model = new AddMovieViewModel()
            {
                Title = existMovie.Title,
                Description = existMovie.Description,
                Cast = existMovie.Cast,
                Director = existMovie.Director,
                Duration = existMovie.Duration,
                Rating = existMovie.Rating,
                ReleaseDate = existMovie.ReleaseDate.ToString(DateFormatOfMovie),
                PosterUrl = existMovie.PosterUrl,
                TrailerUrl = existMovie.TrailerUrl,
                GenreId = existMovie.GenreId
            };

            model.Genres = GetTypes();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(AddMovieViewModel model, int id)
        {

            if (!ModelState.IsValid)
            {
                model.Genres = GetTypes();
                return View(model);
            }

            DateTime start;

            if (!DateTime.TryParseExact(
               model.ReleaseDate,
               DateFormatOfMovie,
               CultureInfo.InvariantCulture,
               DateTimeStyles.None,
               out start))
            {

                ModelState
                    .AddModelError(nameof(model.ReleaseDate), $"Invalid date! Format must be: {DateFormatOfMovie}");

                model.Genres = GetTypes();

                return View(model);
            }

            var entity = await _context.Movies.FindAsync(id);

            if (entity == null)
            {
                throw new ArgumentException("Invalid Id");
            }

            //string currentUserId = GetUserId() ?? string.Empty;

            //if (entity.CreatedByUserId != currentUserId)
            //{
            //    RedirectToAction(nameof(Index));
            //}

            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.Cast = model.Cast;
            entity.Director = model.Director;
            entity.Duration = model.Duration;
            entity.Rating = model.Rating;
            entity.ReleaseDate = start;
            entity.PosterUrl = model.PosterUrl;
            entity.TrailerUrl = model.TrailerUrl ?? String.Empty;
            entity.GenreId = model.GenreId;

            await _context.SaveChangesAsync();



            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(string? id)
        {
            var model = await _context.Movies
           .Where(p => p.Id.ToString() == id)
           .AsNoTracking()
           .Select(p => new DeleteMovieViewModel()
           {
               Id = p.Id,
               Title = p.Title,
               GenreName = p.Genre.Name,
               PosterUrl = p.PosterUrl,
               Rating = p.Rating,
               ReleaseDate = p.ReleaseDate
           })
           .FirstOrDefaultAsync();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(DeleteMovieViewModel model)
        {
            var product = await _context.Movies
                .Where(g => g.Id == model.Id)
                .FirstOrDefaultAsync();

            if (model != null)
            {
                product.IsDeleted = true;

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        private IEnumerable<TypeOfGenreMovies> GetTypes()
        {
            return _context.Genres
                .Select(x => new TypeOfGenreMovies
                {
                    Id = x.Id,
                    Name = x.Name
                });
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
