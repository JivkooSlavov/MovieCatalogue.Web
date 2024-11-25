using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Web.ViewModels.Rating;
using System.Security.Claims;

namespace MovieCatalogue.Web.Controllers
{
    [Authorize]
    public class RatingController : Controller
    {
        private readonly MovieDbContext _context;

        public RatingController(MovieDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(RatingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Movie", new { id = model.MovieId });
            }

            var userId = Guid.Parse(GetUserId());

            var existingRating = await _context.Ratings
                .FirstOrDefaultAsync(r => r.MovieId == model.MovieId && r.UserId == userId);

            if (existingRating != null)
            {
                existingRating.Value = model.Value;
                _context.Ratings.Update(existingRating);
            }
            else
            {
                var rating = new Rating
                {
                    MovieId = model.MovieId,
                    UserId = userId,
                    Value = model.Value
                };

                _context.Ratings.Add(rating);
            }

            await _context.SaveChangesAsync();

            await UpdateMovieRatingAsync(model.MovieId);

            return RedirectToAction("Details", "Movie", new { id = model.MovieId });
        }

        private async Task UpdateMovieRatingAsync(Guid movieId)
        {
            var movie = await _context.Movies
                .Include(m => m.Ratings)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie != null)
            {
                movie.Rating = movie.Ratings.Any()
                    ? movie.Ratings.Average(r => r.Value)
                    : 0;

                _context.Movies.Update(movie);
                await _context.SaveChangesAsync();
            }
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
