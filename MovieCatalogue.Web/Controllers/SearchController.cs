using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Services.Data;
using MovieCatalogue.Services.Data.Interfaces;
using MovieCatalogue.Web.ViewModels.Search;

namespace MovieCatalogue.Web.Controllers
{
    public class SearchController : BaseController
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string query)
        {
            var viewModel = new SearchViewModel
            {
                Query = query,
                Movies = await _searchService.SearchMoviesAsync(query)
            };

            return View(viewModel);
        }
    }
}
