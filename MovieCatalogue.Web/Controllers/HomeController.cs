using Microsoft.AspNetCore.Mvc;
using MovieCatalogue.Services.Data.Interfaces;
using MovieCatalogue.Web.Viewmodels;
using MovieCatalogue.Web.ViewModels.Home;
using System.Diagnostics;

namespace MovieCatalogue.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;

        public HomeController(ILogger<HomeController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var popularMovies = await _movieService.GetPopularMoviesAsync();

            var model = new HomeIndexViewModel
            {
                PopularMovies = popularMovies
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error(int? statusCode = null)
        {
            if (!statusCode.HasValue)
            {
                return this.View("Error");
            }

            if (statusCode == 404)
            {
                return this.View("Error404");
            }
            else if (statusCode == 401 || statusCode == 403)
            {
                return this.View("Error403");
            }

            return this.View("Error500");
        }

    }
}
