using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Common;
using MovieCatalogue.Data;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Web.ViewModels.Movie;
using static MovieCatalogue.Common.EntityValidationConstants;

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
        public async Task<IActionResult> Index()
        {
            var model = await _context.Movies
                  .Where(x => x.IsDeleted == false)
                  .Include(y => y.Genre)
                .Select(x => new MovieInfoViewModel
                {
                    Cast = x.Cast,
                    Description = x.Description,
                    Director = x.Director,
                    Duration = x.Duration,
                    PosterUrl = x.PosterUrl,
                    Genre = x.Genre.Name,
                    Rating = x.Rating,
                    ReleaseDate = x.ReleaseDate.ToString(EntityValidationConstants.Movie.DateFormatOfMovie),
                    Title = x.Title,
                    TrailerUrl = x.TrailerUrl,
                })
                .AsNoTracking()
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                RedirectToAction(nameof(Index));
            }
            var movie = await _context.Movies
                .Include(m => m.Genre)
                    .Include(m => m.Reviews)
                .ThenInclude(r => r.User)
                    .Include(m => m.Ratings)
                .ThenInclude(r => r.User)
                .Where(e => e.IsDeleted == false)
                .Select(e => new MovieInfoViewModel()
                {
                    Id = e.Id.ToString(),
                    Cast = e.Cast,
                    Description = e.Description,
                    Director = e.Director,
                    Duration = e.Duration,
                    PosterUrl = e.PosterUrl,
                    Genre = e.Genre.Name,
                    Rating = e.Rating,
                    ReleaseDate = e.ReleaseDate.ToString(EntityValidationConstants.Movie.DateFormatOfMovie),
                    Title = e.Title,
                    TrailerUrl = e.TrailerUrl,
                })

                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (movie == null)
            {
                RedirectToAction(nameof(Index));
            }


            return View(movie);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new AddMovieViewModel
            {
                Genres = GetTypes()
            };
            return View(model);
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
    }
}
