using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Data.Repository;
using MovieCatalogue.Data.Repository.Interfaces;
using MovieCatalogue.Services.Data;
using MovieCatalogue.Services.Data.Interfaces;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCatalogue.Tests
{
    [TestFixture]
    public class SearchServiceTests
    {
        private DbContextOptions<MovieDbContext> _options;
        private MovieDbContext _context;
        private ISearchService _searchService;

        [SetUp]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<MovieDbContext>()
                .UseInMemoryDatabase(databaseName: "MovieCatalogueTestDb")
                .Options;

            _context = new MovieDbContext(_options);

            var movieRepo = new BaseRepository<Movie, Guid>(_context);
            _searchService = new SearchService(movieRepo);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestCase("Test", 1)]
        [TestCase("NonExistentQuery", 0)]
        public async Task SearchMoviesAsync_ReturnsCorrectResults_BasedOnQuery(string query, int expectedCount)
        {
            // Adding test data
            var genre = new Genre { Name = "Action" };
            var movie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = "Test Movie",
                Genre = genre,
                GenreId = genre.Id,
                Description = "A test movie description.",
                ReleaseDate = DateTime.Today,
                Cast = "Actor 1, Actor 2",
                TrailerUrl = "http://test.com/trailer",
                PosterUrl = "http://test.com/poster.jpg",
                Director = "Director Name",
                Duration = 120,
                IsDeleted = false,
                CreatedByUserId = Guid.NewGuid(),
            };
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            var result = await _searchService.SearchMoviesAsync(query);

            Assert.AreEqual(expectedCount, result.Count());
            if (expectedCount > 0)
            {
                Assert.AreEqual("Test Movie", result.First().Title);
            }
        }

        [Test]
        public async Task SearchMoviesAsync_ReturnsEmpty_WhenQueryDoesNotMatch()
        {
            var genre = new Genre { Name = "Drama" };
            var movie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = "Another Movie",
                Genre = genre,
                GenreId = genre.Id,
                Description = "A test movie description.",
                ReleaseDate = DateTime.Today,
                Cast = "Actor 1, Actor 2",
                TrailerUrl = "http://test.com/trailer",
                PosterUrl = "http://test.com/poster.jpg",
                Director = "Director 2",
                Duration = 120,
                IsDeleted = false,
                CreatedByUserId = Guid.NewGuid(),
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            var result = await _searchService.SearchMoviesAsync("Test");

            Assert.IsEmpty(result);
        }

        [Test]
        public async Task SearchMoviesAsync_ReturnsOnlyNotDeletedMovies()
        {
            var genre = new Genre { Name = "Action" };

            var activeMovie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = "Active Movie",
                Genre = genre,
                GenreId = genre.Id,
                Description = "An active movie description.",
                ReleaseDate = DateTime.Today,
                Cast = "Actor 1",
                TrailerUrl = "http://test.com/trailer1",
                PosterUrl = "http://test.com/poster1.jpg",
                Director = "Director 1",
                Duration = 100,
                IsDeleted = false,
                CreatedByUserId = Guid.NewGuid(),
            };

            var deletedMovie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = "Deleted Movie",
                Genre = genre,
                GenreId = genre.Id,
                Description = "A deleted movie description.",
                ReleaseDate = DateTime.Today,
                Cast = "Actor 2",
                TrailerUrl = "http://test.com/trailer2",
                PosterUrl = "http://test.com/poster2.jpg",
                Director = "Director 2",
                Duration = 90,
                IsDeleted = true,
                CreatedByUserId = Guid.NewGuid(),
            };

            _context.Movies.Add(activeMovie);
            _context.Movies.Add(deletedMovie);
            await _context.SaveChangesAsync();

            var result = await _searchService.SearchMoviesAsync("Movie");

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Active Movie", result.First().Title);
        }

        [TestCase("", 0)]
        [TestCase("   ", 0)]
        public async Task SearchMoviesAsync_ReturnsEmpty_WhenQueryIsEmptyOrWhitespace(string query, int expectedCount)
        {
            var result = await _searchService.SearchMoviesAsync(query);

            Assert.AreEqual(expectedCount, result.Count());
        }
    }
}
