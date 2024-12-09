using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Data.Repository;
using MovieCatalogue.Services.Data;
using MovieCatalogue.Web.ViewModels.Review;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCatalogue.Tests
{
    [TestFixture]
    public class ReviewServiceTests
    {
        private DbContextOptions<MovieDbContext> _options;
        private MovieDbContext _context;
        private ReviewService _reviewService;

        [SetUp]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<MovieDbContext>()
                .UseInMemoryDatabase(databaseName: "MovieCatalogueTestDb")
                .Options;
            _context = new MovieDbContext(_options);

            var reviewRepository = new BaseRepository<Review, Guid>(_context);
            var movieRepository = new BaseRepository<Movie, Guid>(_context);
            _reviewService = new ReviewService(reviewRepository, movieRepository);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetReviewsForMovieAsync_ReturnsCorrectReviews()
        {
            var movie = await CreateMovieAsync("Movie 1"); 
            var review = await CreateReviewAsync(movie.Id); 

            var result = await _reviewService.GetReviewsForMovieAsync(movie.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Reviews.Count);
            Assert.AreEqual(review.Content, result.Reviews.First().Content);
            Assert.AreEqual(movie.Id, result.MovieId);
            Assert.AreEqual(movie.Title, result.MovieTitle);
        }

        [Test]
        public async Task CreateReviewAsync_ReturnsTrue_WhenSuccessfullyCreated()
        {
            var movie = await CreateMovieAsync("New Movie");
            var reviewModel = new ReviewCreateViewModel
            {
                MovieId = movie.Id,
                Content = "This is a review."
            };
            var userId = Guid.NewGuid();

            var result = await _reviewService.CreateReviewAsync(reviewModel, userId);

            Assert.IsTrue(result);
            Assert.AreEqual(1, _context.Reviews.Count());
        }

        [Test]
        public async Task GetReviewForEditAsync_ReturnsCorrectReview_WhenValid()
        {
            var movie = await CreateMovieAsync("Movie 1");
            var review = await CreateReviewAsync(movie.Id);
            var userId = review.UserId;

            var result = await _reviewService.GetReviewForEditAsync(review.Id, userId, isAdmin: false);

            Assert.IsNotNull(result);
            Assert.AreEqual(review.Content, result?.Content);
        }

        [Test]
        public async Task UpdateReviewAsync_ReturnsTrue_WhenSuccessfullyUpdated()
        {
            var movie = await CreateMovieAsync("Movie 1");
            var review = await CreateReviewAsync(movie.Id);
            var reviewModel = new ReviewEditViewModel
            {
                Id = review.Id,
                Content = "Updated review content."
            };
            var userId = review.UserId;

            var result = await _reviewService.UpdateReviewAsync(reviewModel, userId, isAdmin: false);

            Assert.IsTrue(result);
            var updatedReview = _context.Reviews.FirstOrDefault(r => r.Id == review.Id);
            Assert.AreEqual("Updated review content.", updatedReview?.Content);
        }

        [Test]
        public async Task DeleteReviewAsync_ReturnsTrue_WhenSuccessfullyDeleted()
        {
            var movie = await CreateMovieAsync("Movie 1");
            var review = await CreateReviewAsync(movie.Id);
            var userId = review.UserId;

            var result = await _reviewService.DeleteReviewAsync(review.Id, userId, isAdmin: false);

            Assert.IsTrue(result);
            var deletedReview = _context.Reviews.FirstOrDefault(r => r.Id == review.Id);
            Assert.IsTrue(deletedReview?.IsDeleted ?? false);
        }

        [Test]
        public async Task GetTotalReviewsAsync_ReturnsCorrectCount()
        {
            await CreateReviewAsync(Guid.NewGuid());
            await CreateReviewAsync(Guid.NewGuid());

            var result = await _reviewService.GetTotalReviewsAsync();

            Assert.AreEqual(2, result);
        }
        [Test]
        public async Task GetReviewsForMovieAsync_ReturnsNull_WhenMovieDoesNotExist()
        {
            var result = await _reviewService.GetReviewsForMovieAsync(Guid.NewGuid());

            Assert.IsNull(result);
        }

        [Test]
        public async Task UpdateReviewAsync_ReturnsFalse_WhenReviewDoesNotExist()
        {
            var reviewModel = new ReviewEditViewModel
            {
                Id = Guid.NewGuid(),
                Content = "Updated review content."
            };
            var userId = Guid.NewGuid();

            var result = await _reviewService.UpdateReviewAsync(reviewModel, userId, isAdmin: false);

            Assert.IsFalse(result);
        }
        [Test]
        public async Task GetReviewForEditAsync_ReturnsNull_WhenUserIsNotOwner()
        {
            var movie = await CreateMovieAsync("Movie 1");
            var review = await CreateReviewAsync(movie.Id);
            var anotherUserId = Guid.NewGuid(); 

            var result = await _reviewService.GetReviewForEditAsync(review.Id, anotherUserId, isAdmin: false);

            Assert.IsNull(result);
        }

        [Test]
        public async Task DeleteReviewAsync_ReturnsFalse_WhenReviewDoesNotExist()
        {
            var result = await _reviewService.DeleteReviewAsync(Guid.NewGuid(), Guid.NewGuid(), isAdmin: false);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteReviewAsync_ReturnsFalse_WhenUserIsNotOwner()
        {
            var movie = await CreateMovieAsync("Movie 1");
            var review = await CreateReviewAsync(movie.Id);
            var anotherUserId = Guid.NewGuid();

            var result = await _reviewService.DeleteReviewAsync(review.Id, anotherUserId, isAdmin: false);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetReviewForEditAsync_ReturnsNull_WhenReviewDoesNotExist()
        {
            var result = await _reviewService.GetReviewForEditAsync(Guid.NewGuid(), Guid.NewGuid(), isAdmin: false);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetReviewForEditAsync_ReturnsNull_WhenUserIsNotOwnerOrAdmin()
        {
            var movie = await CreateMovieAsync("Movie 1");
            var review = await CreateReviewAsync(movie.Id);
            var anotherUserId = Guid.NewGuid();

            var result = await _reviewService.GetReviewForEditAsync(review.Id, anotherUserId, isAdmin: false);

            Assert.IsNull(result);
        }

        private async Task<Movie> CreateMovieAsync(string title)
        {
            var genreEntity = new Genre { Name = "Action" };
            _context.Genres.Add(genreEntity);
            await _context.SaveChangesAsync();

            var movie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = title,
                Genre = genreEntity,
                GenreId = genreEntity.Id,
                Description = "Test description",
                ReleaseDate = DateTime.Today,
                Cast = "Actor 1, Actor 2",
                TrailerUrl = "http://test.com/trailer",
                PosterUrl = "http://test.com/poster.jpg",
                Director = "Test Director",
                Duration = 120,
                IsDeleted = false,
                CreatedByUserId = Guid.NewGuid()
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        private async Task<Review> CreateReviewAsync(Guid movieId)
        {   
            var review = new Review
            {
                Id = Guid.NewGuid(),
                Content = "Test review content",
                MovieId = movieId,
                UserId = Guid.NewGuid(),
                DatePosted = DateTime.Now
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }
    }
}
