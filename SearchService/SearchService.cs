using Moq;
using Microsoft.AspNetCore.Mvc;
using MovieCatalogue.Web.Controllers;
using MovieCatalogue.Services.Data.Interfaces;
using MovieCatalogue.Web.ViewModels.Search;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MovieCatalogue.Tests.Controllers
{
    public class SearchControllerTests
    {
        private readonly Mock<ISearchService> _mockSearchService;
        private readonly SearchController _controller;

        public SearchControllerTests()
        {
            // Създаваме мок за ISearchService
            _mockSearchService = new Mock<ISearchService>();

            // Създаваме контролера с мокнатата услуга
            _controller = new SearchController(_mockSearchService.Object);
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithSearchResults()
        {
            // Arrange
            var query = "Test Movie";
            var mockMovies = new List<MovieSearchResultViewModel>
            {
                new MovieSearchResultViewModel { Title = "Test Movie 1" },
                new MovieSearchResultViewModel { Title = "Test Movie 2" }
            };

            // Настройваме мок услугата да връща стойности
            _mockSearchService.Setup(service => service.SearchMoviesAsync(query))
                .ReturnsAsync(mockMovies);

            // Act
            var result = await _controller.Index(query);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result); // Проверяваме, че връщаният резултат е ViewResult
            var model = Assert.IsAssignableFrom<SearchViewModel>(viewResult.Model); // Проверяваме модела

            Assert.Equal(query, model.Query); // Проверяваме дали Query е правилно зададено
            Assert.Equal(mockMovies.Count, model.Movies.Count); // Проверяваме дали броят на филмите съвпада с очаквания
        }

        [Fact]
        public async Task Index_ReturnsEmptyMovies_WhenQueryIsNullOrEmpty()
        {
            // Arrange
            var query = "";

            // Настройваме мок услугата да връща празен списък
            _mockSearchService.Setup(service => service.SearchMoviesAsync(query))
                .ReturnsAsync(new List<MovieSearchResultViewModel>());

            // Act
            var result = await _controller.Index(query);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<SearchViewModel>(viewResult.Model);

            Assert.Empty(model.Movies); // Проверяваме дали няма филми, когато заявката е празна
        }
    }
}
