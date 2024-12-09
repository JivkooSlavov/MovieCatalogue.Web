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
            // ��������� ��� �� ISearchService
            _mockSearchService = new Mock<ISearchService>();

            // ��������� ���������� � ��������� ������
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

            // ����������� ��� �������� �� ����� ���������
            _mockSearchService.Setup(service => service.SearchMoviesAsync(query))
                .ReturnsAsync(mockMovies);

            // Act
            var result = await _controller.Index(query);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result); // �����������, �� ��������� �������� � ViewResult
            var model = Assert.IsAssignableFrom<SearchViewModel>(viewResult.Model); // ����������� ������

            Assert.Equal(query, model.Query); // ����������� ���� Query � �������� ��������
            Assert.Equal(mockMovies.Count, model.Movies.Count); // ����������� ���� ����� �� ������� ������� � ���������
        }

        [Fact]
        public async Task Index_ReturnsEmptyMovies_WhenQueryIsNullOrEmpty()
        {
            // Arrange
            var query = "";

            // ����������� ��� �������� �� ����� ������ ������
            _mockSearchService.Setup(service => service.SearchMoviesAsync(query))
                .ReturnsAsync(new List<MovieSearchResultViewModel>());

            // Act
            var result = await _controller.Index(query);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<SearchViewModel>(viewResult.Model);

            Assert.Empty(model.Movies); // ����������� ���� ���� �����, ������ �������� � ������
        }
    }
}
