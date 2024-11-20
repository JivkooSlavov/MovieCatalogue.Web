using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data;
using MovieCatalogue.Data.Models;
using System.Security.Claims;
using MovieCatalogue.Web.ViewModels;
using MovieCatalogue.Web.ViewModels.Favorite;
using MovieCatalogue.Web.ViewModels.Movie;

namespace MovieCatalogue.Web.Controllers
{
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
        [ValidateAntiForgeryToken]
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromFavorite(int id)
        {
            var favorite = await _context.Favorites.FindAsync(id);

            if (favorite == null || favorite.UserId != Guid.Parse(GetUserId()))
            {
                return NotFound();
            }

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
            private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
