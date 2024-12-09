using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Data.Repository;
using MovieCatalogue.Services.Data;
using MovieCatalogue.Web.ViewModels.Movie;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCatalogue.Tests
{
    [TestFixture]
    public class MovieServiceTests
    {
        private DbContextOptions<MovieDbContext> _options;
        private MovieDbContext _context;
        private MovieService _movieService;

        [SetUp]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<MovieDbContext>()
                .UseInMemoryDatabase(databaseName: "MovieCatalogueTestDb")
                .Options;
            _context = new MovieDbContext(_options);

            var movieRepository = new BaseRepository<Movie, Guid>(_context);
            var genreRepository = new BaseRepository<Genre, Guid>(_context);
            var ratingRepository = new BaseRepository<Rating, Guid>(_context);
            _movieService = new MovieService(movieRepository, genreRepository, ratingRepository);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetMoviesByPageAsync_ReturnsCorrectMovies()
        {
            await CreateMovieAsync("Movie 1");
            await CreateMovieAsync("Movie 2");

            var result = await _movieService.GetMoviesByPageAsync(1, 2);

            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(m => m.Title == "Movie 1"));
            Assert.IsTrue(result.Any(m => m.Title == "Movie 2"));
        }

        [Test]
        public async Task GetTotalMoviesAsync_ReturnsCorrectCount()
        {
            await CreateMovieAsync("Movie 1");
            await CreateMovieAsync("Movie 2");

            var result = await _movieService.GetTotalMoviesAsync();

            Assert.AreEqual(2, result);
        }

        [Test]
        public async Task GetMovieDetailsAsync_ReturnsCorrectMovieDetails()
        {
            var movie = await CreateMovieAsync("Movie 1");

            var result = await _movieService.GetMovieDetailsAsync(movie.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(movie.Title, result?.Title);
            Assert.AreEqual(movie.Description, result?.Description);
        }

        [Test]
        public async Task GetMovieDetailsAsync_ReturnsNull_WhenMovieDoesNotExist()
        {
            var nonExistentMovieId = Guid.NewGuid();

            var result = await _movieService.GetMovieDetailsAsync(nonExistentMovieId);

            Assert.IsNull(result);
        }

        [Test]
        public async Task AddMovieAsync_ReturnsTrue_WhenSuccessfullyAdded()
        {
            var model = new AddMovieViewModel
            {
                Title = "New Movie",
                Description = "New Description",
                GenreId = Guid.NewGuid(),
                ReleaseDate = DateTime.Today.ToString("yyyy-MM-dd"),
                Cast = "Actor A, Actor B",
                Director = "Director A",
                Duration = 120,
                PosterUrl = "http://poster.url",
                TrailerUrl = "http://trailer.url",
                Rating = 4.5
            };
            var userId = Guid.NewGuid();

            var result = await _movieService.AddMovieAsync(model, userId);

            Assert.IsTrue(result);
            Assert.AreEqual(1, _context.Movies.Count());
        }

        [Test]
        public async Task EditMovieAsync_ReturnsTrue_WhenSuccessfullyEdited()
        {
            var movie = await CreateMovieAsync("Original Title");
            var model = new AddMovieViewModel
            {
                Id = movie.Id,
                Title = "Updated Title",
                Description = "Updated Description",
                GenreId = movie.GenreId,
                ReleaseDate = DateTime.Today.ToString("yyyy-MM-dd"),
                Cast = "Updated Cast",
                Director = "Updated Director",
                Duration = 150,
                PosterUrl = "http://updated.poster",
                TrailerUrl = "http://updated.trailer",
                Rating = 5.0
            };
            var userId = movie.CreatedByUserId;

            var result = await _movieService.EditMovieAsync(movie.Id, model, userId, isAdmin: false);

            Assert.IsTrue(result);
            var updatedMovie = _context.Movies.FirstOrDefault(m => m.Id == movie.Id);
            Assert.IsNotNull(updatedMovie);
            Assert.AreEqual("Updated Title", updatedMovie?.Title);
        }

        [Test]
        public async Task EditMovieAsync_ReturnsFalse_WhenMovieDoesNotExist()
        {
            var nonExistentMovieId = Guid.NewGuid();
            var model = new AddMovieViewModel
            {
                Id = nonExistentMovieId,
                Title = "Updated Title",
                Description = "Updated Description",
                GenreId = Guid.NewGuid(),
                ReleaseDate = DateTime.Today.ToString("yyyy-MM-dd"),
                Cast = "Updated Cast",
                Director = "Updated Director",
                Duration = 150,
                PosterUrl = "http://updated.poster",
                TrailerUrl = "http://updated.trailer",
                Rating = 5.0
            };
            var userId = Guid.NewGuid();

            var result = await _movieService.EditMovieAsync(nonExistentMovieId, model, userId, isAdmin: false);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteMovieAsync_ReturnsTrue_WhenSuccessfullyDeleted()
        {
            var movie = await CreateMovieAsync("Deletable Movie");
            var userId = movie.CreatedByUserId;

            var result = await _movieService.DeleteMovieAsync(movie.Id, userId, isAdmin: false);

            Assert.IsTrue(result);
            Assert.IsTrue(_context.Movies.First(m => m.Id == movie.Id).IsDeleted);
        }

        [Test]
        public async Task DeleteMovieAsync_ReturnsFalse_WhenMovieDoesNotExist()
        {
            var nonExistentMovieId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var result = await _movieService.DeleteMovieAsync(nonExistentMovieId, userId, isAdmin: false);

            Assert.IsFalse(result);
        }


        [Test]
        public async Task GetPopularMoviesAsync_ReturnsTopRatedMovies()
        {
            var movie1 = await CreateMovieAsync("Movie 1");
            var movie2 = await CreateMovieAsync("Movie 2");
            movie1.Rating = 4.5;
            movie2.Rating = 5.0;
            _context.UpdateRange(movie1, movie2);
            await _context.SaveChangesAsync();

            var result = await _movieService.GetPopularMoviesAsync();

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Movie 2", result.First().Title);
        }

        private async Task<Movie> CreateMovieAsync(string title)
        {
            var genre = new Genre { Name = "Action" };
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            var movie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = title,
                Genre = genre,
                GenreId = genre.Id,
                Description = "Test description",
                ReleaseDate = DateTime.Today,
                Cast = "Actor 1, Actor 2",
                TrailerUrl = "http://test.com/trailer",
                PosterUrl = "http://test.com/poster.jpg",
                Director = "Test Director",
                Duration = 120,
                CreatedByUserId = Guid.NewGuid()
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }
    }
}
