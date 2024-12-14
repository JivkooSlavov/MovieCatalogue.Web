using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Data.Repository;
using MovieCatalogue.Services.Data;
using MovieCatalogue.Web.ViewModels.Movie;
using static MovieCatalogue.Common.EntityValidationConstants.MovieConstants;

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
            var reviewRepository = new BaseRepository<Review, Guid>(_context);
            var favoriteRepository = new BaseRepository<Favorite, Guid>(_context);
            _movieService = new MovieService(movieRepository, genreRepository, ratingRepository,reviewRepository,favoriteRepository);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestCase(1, 2, 2)]
        [TestCase(1, 1, 1)]
        public async Task GetMoviesByPageAsync_ReturnsCorrectMovies(int page, int pageSize, int expectedCount)
        {
            await CreateMovieAsync("Movie 1");
            await CreateMovieAsync("Movie 2");

            var result = await _movieService.GetMoviesByPageAsync(page, pageSize);

            Assert.AreEqual(expectedCount, result.Count());
        }

        [Test]
        public async Task GetTotalMoviesAsync_ReturnsCorrectCount()
        {
            await CreateMovieAsync("Movie 1");
            await CreateMovieAsync("Movie 2");

            var result = await _movieService.GetTotalMoviesAsync();

            Assert.AreEqual(2, result);
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task GetMovieDetailsAsync_ReturnsExpectedResult(bool movieExists)
        {
            var movieId = movieExists ? (await CreateMovieAsync("Existing Movie")).Id : Guid.NewGuid();

            var result = await _movieService.GetMovieDetailsAsync(movieId);

            if (movieExists)
            {
                Assert.IsNotNull(result);
                Assert.AreEqual("Existing Movie", result?.Title);
            }
            else
            {
                Assert.IsNull(result);
            }
        }

        [Test]
        public async Task AddMovieAsync_ReturnsTrue_WhenSuccessfullyAdded()
        {
            var model = GetTestAddMovieViewModel();
            var userId = Guid.NewGuid();

            var result = await _movieService.AddMovieAsync(model, userId);

            Assert.IsTrue(result);
            Assert.AreEqual(1, _context.Movies.Count());
        }
        [Test]
        public async Task GetMovieForEditAsync_ReturnsMovie_WhenAuthorizedUser()
        {
            var movie = await CreateMovieAsync("Movie to Edit");
            var userId = movie.CreatedByUserId;

            var result = await _movieService.GetMovieForEditAsync(movie.Id, userId, isAdmin: false);

            Assert.IsNotNull(result);
            Assert.AreEqual(movie.Title, result?.Title);
        }

        [Test]
        public async Task GetMovieForEditAsync_ReturnsNull_WhenNotAuthorizedUser()
        {
            var movie = await CreateMovieAsync("Movie to Edit");
            var unauthorizedUserId = Guid.NewGuid();

            var result = await _movieService.GetMovieForEditAsync(movie.Id, unauthorizedUserId, isAdmin: false);

            Assert.IsNull(result);
        }
        [Test]
        public async Task GetMovieForDeletionAsync_ReturnsMovie_WhenAuthorizedUser()
        {
            var movie = await CreateMovieAsync("Movie to Delete");
            var userId = movie.CreatedByUserId;

            var result = await _movieService.GetMovieForDeletionAsync(movie.Id, userId, isAdmin: false);

            Assert.IsNotNull(result);
            Assert.AreEqual(movie.Title, result?.Title);
        }

        [Test]
        public async Task GetMovieForDeletionAsync_ReturnsNull_WhenNotAuthorizedUser()
        {
            var movie = await CreateMovieAsync("Movie to Delete");
            var unauthorizedUserId = Guid.NewGuid();

            var result = await _movieService.GetMovieForDeletionAsync(movie.Id, unauthorizedUserId, isAdmin: false);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetMovieForDeletionAsync_ReturnsMovie_WhenAdmin()
        {
            var movie = await CreateMovieAsync("Movie to Delete");
            var adminUserId = Guid.NewGuid();

            var result = await _movieService.GetMovieForDeletionAsync(movie.Id, adminUserId, isAdmin: true);

            Assert.IsNotNull(result);
            Assert.AreEqual(movie.Title, result?.Title);
        }

        [Test]
        public async Task DeleteMovieAsync_ReturnsTrue_WhenAuthorizedUser()
        {
            var movie = await CreateMovieAsync("Movie to Delete");
            var userId = movie.CreatedByUserId;

            var result = await _movieService.DeleteMovieAsync(movie.Id, userId, isAdmin: false);

            Assert.IsTrue(result);
            var deletedMovie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == movie.Id);
            Assert.IsTrue(deletedMovie?.IsDeleted);
        }

        [Test]
        public async Task DeleteMovieAsync_ReturnsFalse_WhenNotAuthorizedUser()
        {
            var movie = await CreateMovieAsync("Movie to Delete");
            var unauthorizedUserId = Guid.NewGuid();

            var result = await _movieService.DeleteMovieAsync(movie.Id, unauthorizedUserId, isAdmin: false);

            Assert.IsFalse(result);
            var notDeletedMovie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == movie.Id);
            Assert.IsFalse(notDeletedMovie?.IsDeleted);
        }

        [Test]
        public async Task DeleteMovieAsync_ReturnsTrue_WhenAdmin()
        {
            var movie = await CreateMovieAsync("Movie to Delete");
            var adminUserId = Guid.NewGuid();

            var result = await _movieService.DeleteMovieAsync(movie.Id, adminUserId, isAdmin: true);

            Assert.IsTrue(result);
            var deletedMovie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == movie.Id);
            Assert.IsTrue(deletedMovie?.IsDeleted);
        }

        [TestCase(true, true)]
        [TestCase(false, false)]
        public async Task EditMovieAsync_ReturnsExpectedResult(bool movieExists, bool expectedResult)
        {
            var movieId = movieExists ? (await CreateMovieAsync("Original Title")).Id : Guid.NewGuid();
            var model = GetTestEditMovieViewModel(movieId);
            var userId = movieExists ? _context.Movies.First().CreatedByUserId : Guid.NewGuid();

            var result = await _movieService.EditMovieAsync(movieId, model, userId, isAdmin: false);

            Assert.AreEqual(expectedResult, result);
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

        private AddMovieViewModel GetTestAddMovieViewModel() => new()
        {
            Title = "New Movie",
            Description = "New Description",
            GenreId = Guid.NewGuid(),
            ReleaseDate = DateTime.Today.ToString(DateFormatOfMovie),
            Cast = "Actor A, Actor B",
            Director = "Director A",
            Duration = 120,
            PosterUrl = "http://poster.url",
            TrailerUrl = "http://trailer.url",
            Rating = 4.5
        };

        private AddMovieViewModel GetTestEditMovieViewModel(Guid movieId) => new()
        {
            Id = movieId,
            Title = "Updated Title",
            Description = "Updated Description",
            GenreId = Guid.NewGuid(),
            ReleaseDate = DateTime.Today.ToString(DateFormatOfMovie),
            Cast = "Updated Cast",
            Director = "Updated Director",
            Duration = 150,
            PosterUrl = "http://updated.poster",
            TrailerUrl = "http://updated.trailer",
            Rating = 5.0
        };

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