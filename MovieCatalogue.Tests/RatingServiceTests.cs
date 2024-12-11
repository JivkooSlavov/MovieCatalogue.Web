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
        [TestCase(0, 5, 5)]
        [TestCase(3, 5, 5)]
        public async Task AddOrUpdateRatingAsync_AddsOrUpdatesRating_Correctly(int initialRating, int newRating, int expectedRating)
        {
            var movie = await CreateTestMovieAsync();
            var userId = Guid.NewGuid();

            if (initialRating > 0)
            {
                await AddTestRatingAsync(movie.Id, userId, initialRating);
            }

            await _ratingService.AddOrUpdateRatingAsync(movie.Id, userId, newRating);

            var rating = await _ratingRepository.FirstOrDefaultAsync(r => r.MovieId == movie.Id && r.UserId == userId);
            Assert.IsNotNull(rating);
            Assert.AreEqual(expectedRating, rating.Value);
        }

        [Test]
        [TestCase(new int[] { 5, 3 }, 4)]
        [TestCase(new int[] { }, 0)] 
        [TestCase(new int[] { 4 }, 4)]
        public async Task UpdateMovieRatingAsync_CalculatesCorrectAverageRating(int[] ratings, double expectedAverage)
        {
            var movie = await CreateTestMovieAsync();

            foreach (var rating in ratings)
            {
                await AddTestRatingAsync(movie.Id, Guid.NewGuid(), rating);
            }

            await _ratingService.UpdateMovieRatingAsync(movie.Id);

            var updatedMovie = await _movieRepository.FirstOrDefaultAsync(m => m.Id == movie.Id);
            Assert.IsNotNull(updatedMovie);
            Assert.AreEqual(expectedAverage, updatedMovie.Rating);
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

        private async Task AddTestRatingAsync(Guid movieId, Guid userId, int value)
        {
            var rating = new Rating
            {
                Id = Guid.NewGuid(),
                MovieId = movieId,
                UserId = userId,
                Value = value
            };
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
        }
    }
}
