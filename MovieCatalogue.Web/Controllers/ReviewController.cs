using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Web.ViewModels;
using MovieCatalogue.Web.ViewModels.Movie;
using MovieCatalogue.Web.ViewModels.Review;
using System.Security.Claims;

namespace MovieCatalogue.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly MovieDbContext _context;
        private readonly UserManager<User> _userManager;

        public ReviewController(MovieDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(Guid movieId)
        {
            var movie = await _context.Movies
             .Include(m => m.Reviews)
             .ThenInclude(r => r.User)
             .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null)
            {
                return NotFound();
            }

            var model = new ReviewIndexViewModel
            {
                MovieId = movie.Id,
                MovieTitle = movie.Title,
                Reviews = movie.Reviews
                 .OrderByDescending(r => r.DatePosted)
                .Select(r => new ReviewViewModel
                {
                    Id = r.Id,
                    UserName = r.User.UserName ?? string.Empty,
                    Content = r.Content,
                    CreatedAt = r.DatePosted
                }).ToList()
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create(Guid movieId)
        {
            ReviewCreateViewModel reviewMovie = new ReviewCreateViewModel
            {
                MovieId = movieId
            };

            return View(reviewMovie);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ReviewCreateViewModel reviewVm)
        {
            if (!ModelState.IsValid)
            {
                return View(reviewVm);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            Review review = new Review
            {
                Content = reviewVm.Content,
                MovieId = reviewVm.MovieId,
                UserId = Guid.Parse(userId),
                DatePosted = DateTime.UtcNow,
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { movieId = reviewVm.MovieId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var existReview = await _context.Reviews.FindAsync(id);

            if (existReview == null)
            {
                return BadRequest();
            }

            ReviewCreateViewModel review = new ReviewCreateViewModel
            {
                Id = existReview.Id,
                MovieId = existReview.MovieId,
                Content = existReview.Content,
                UpdatePosted = DateTime.Now,
                CreatedByUserId = existReview.UserId
            };

            Guid currentUserId = Guid.Parse(GetUserId());

            if (review.CreatedByUserId != currentUserId)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ReviewCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var review = await _context.Reviews.FindAsync(model.Id);

            review.Content = model.Content;
            review.UpdatePosted = DateTime.Now;
            

            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { movieId = model.MovieId });
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {

            var model = await _context.Reviews
               .Where(r => r.Id == id)
               .Select(r => new ReviewDeleteViewModel()
               {
                   Id = r.Id,
                   MovieId = r.MovieId,
                   MovieTitle = r.Movie.Title,
                   Content = r.Content,
                   CreatedAt = r.DatePosted,
                   CreatedByUserId = r.UserId
               })
                .FirstOrDefaultAsync();

            Guid currentUserId = Guid.Parse(GetUserId());

            if (model.CreatedByUserId != currentUserId)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteMovieViewModel model)
        {
            var review = await _context.Reviews
                .Where(g => g.Id == model.Id)
                .FirstOrDefaultAsync();

            _context.Reviews.Remove(review);
            _context.SaveChanges();

            return RedirectToAction("Details", "Movie", new { id = review.MovieId });
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
