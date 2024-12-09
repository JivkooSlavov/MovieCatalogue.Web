using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Data.Repository.Interfaces;
using MovieCatalogue.Data.Repository;
using MovieCatalogue.Data;
using MovieCatalogue.Services.Data.Interfaces;
using MovieCatalogue.Services.Data;

namespace MovieCatalogue.Tests
{
    [TestFixture]
    public class RatingServiceTests
    {
        private DbContextOptions<MovieDbContext> _options;
        private MovieDbContext _context;
        private IRatingService _ratingService;
        private IRepository<Rating, Guid> _ratingRepository;
        private IRepository<Movie, Guid> _movieRepository;

        [SetUp]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<MovieDbContext>()
                .UseInMemoryDatabase(databaseName: "MovieCatalogueTestDb")
                .Options;
            _context = new MovieDbContext(_options);

            _ratingRepository = new BaseRepository<Rating, Guid>(_context);
            _movieRepository = new BaseRepository<Movie, Guid>(_context);
            _ratingService = new RatingService(_ratingRepository, _movieRepository);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddOrUpdateRatingAsync_AddsNewRating_WhenRatingDoesNotExist()
        {
            var movie = await CreateTestMovieAsync();

            await _ratingService.AddOrUpdateRatingAsync(movie.Id, Guid.NewGuid(), 5);

            var rating = await _ratingRepository.FirstOrDefaultAsync(r => r.MovieId == movie.Id);
            Assert.IsNotNull(rating);
            Assert.AreEqual(5, rating.Value);
        }

        [Test]
        public async Task AddOrUpdateRatingAsync_UpdatesExistingRating_WhenRatingAlreadyExists()
        {
            var userId = Guid.NewGuid();
            var movie = await CreateTestMovieAsync();
            var rating = new Rating
            {
                MovieId = movie.Id,
                UserId = userId,
                Value = 3
            };
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            await _ratingService.AddOrUpdateRatingAsync(movie.Id, userId, 5);

            var updatedRating =
                await _ratingRepository.FirstOrDefaultAsync(r => r.MovieId == movie.Id && r.UserId == userId);
            Assert.IsNotNull(updatedRating);
            Assert.AreEqual(5, updatedRating.Value);
        }

        [Test]
        public async Task UpdateMovieRatingAsync_UpdatesMovieRating_Correctly()
        {
            var movie = await CreateTestMovieAsync();
            var rating1 = new Rating { MovieId = movie.Id, Value = 5 };
            var rating2 = new Rating { MovieId = movie.Id, Value = 3 };
            _context.Ratings.Add(rating1);
            _context.Ratings.Add(rating2);
            await _context.SaveChangesAsync();

            await _ratingService.UpdateMovieRatingAsync(movie.Id);

            var updatedMovie = await _movieRepository.FirstOrDefaultAsync(m => m.Id == movie.Id);
            Assert.IsNotNull(updatedMovie);
            Assert.AreEqual(4, updatedMovie.Rating); 
        }

        [Test]
        public async Task UpdateMovieRatingAsync_SetsRatingToZero_WhenNoRatingsExist()
        {
            var movie = await CreateTestMovieAsync();

            await _ratingService.UpdateMovieRatingAsync(movie.Id);

            var updatedMovie = await _movieRepository.FirstOrDefaultAsync(m => m.Id == movie.Id);
            Assert.IsNotNull(updatedMovie);
            Assert.AreEqual(0, updatedMovie.Rating); 
        }

        private async Task<Movie> CreateTestMovieAsync(string title = "Test Movie", bool isDeleted = false)
        {
            var movie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = title,
                Genre = new Genre { Name = "Action" },
                Description = "Test description",
                ReleaseDate = DateTime.Now,
                Cast = "Actor1, Actor2",
                TrailerUrl = "http://test.com/trailer",
                PosterUrl = "http://test.com/poster",
                Director = "Test Director",
                Duration = 120,
                IsDeleted = isDeleted,
                CreatedByUserId = Guid.NewGuid(),
            };
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }
    }
}
