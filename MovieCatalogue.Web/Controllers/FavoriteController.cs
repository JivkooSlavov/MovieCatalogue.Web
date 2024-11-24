using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data;
using MovieCatalogue.Data.Models;
using System.Security.Claims;
using MovieCatalogue.Web.ViewModels;
using MovieCatalogue.Web.ViewModels.Favorite;
using MovieCatalogue.Web.ViewModels.Movie;
using Microsoft.AspNetCore.Authorization;

namespace MovieCatalogue.Web.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly MovieDbContext _context;

        public FavoriteController(MovieDbContext context)
        {
             _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Guid currentUserId = Guid.Parse(GetUserId());


            var favorites = await _context.Favorites
                .Include(f=>f.Movie)
                .Where(f=>f.UserId == currentUserId)
                .Select(f=> new AddMovieToFavorite
                {
                    FavoriteId = f.Id,
                    MovieId = f.Movie.Id,
                    MovieTitle = f.Movie.Title,
                    MovieDescription = f.Movie.Description,
                    PosterUrl = f.Movie.PosterUrl,
                    IsFavorite = true
                })
                .ToListAsync();

            return View(favorites);
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavorites(Guid movieId)
        {
            if (_context.Favorites.Any(f => f.MovieId == movieId && f.UserId == Guid.Parse(GetUserId())))
            {
                return BadRequest("The movie is already in your favorites.");
            }

            var favorite = new Favorite
            {
                MovieId = movieId,
                UserId = Guid.Parse(GetUserId())
            };

            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmRemove(Guid movieId)
        {
            var userId = Guid.Parse(GetUserId());

            var model = await _context.Favorites
                   .Where(f => f.MovieId == movieId && f.UserId == userId) 
                   .Select(f => new RemoveMovieFromFavorite
                   {
                       MovieId = f.MovieId,
                       MovieTitle = f.Movie.Title
                   })
                   .FirstOrDefaultAsync();

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Remove(Guid movieId)
        {
            var userId = Guid.Parse(GetUserId());

            var favorite = await _context.Favorites
                .Include(f => f.Movie)
                .FirstOrDefaultAsync(f => f.MovieId == movieId && f.UserId == userId);

            if (favorite == null)
            {
                return NotFound();
            }

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Favorite");
        }
            private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
