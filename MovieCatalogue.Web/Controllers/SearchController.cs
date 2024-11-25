using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Web.ViewModels.Search;

namespace MovieCatalogue.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly MovieDbContext _context;

        public SearchController(MovieDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string query)
        {
            var viewModel = new SearchViewModel
            {
                Query = query
            };

            if (!string.IsNullOrEmpty(query))
            {
                viewModel.Movies = await _context.Movies
                    .Where(m => m.Title.ToLower().Contains(query.ToLower()))
                    .Select(m => new MovieSearchResultViewModel
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Genre = m.Genre.Name,
                        PosterUrl = m.PosterUrl,
                        Director = m.Director
                    })
                    .ToListAsync();
            }

            return View(viewModel);
        }
    }
}
