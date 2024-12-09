using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Data.Repository;
using MovieCatalogue.Data.Repository.Interfaces;
using MovieCatalogue.Services.Data;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCatalogue.Tests
{
    [TestFixture]
    public class FavoriteServiceTests
    {
        private DbContextOptions<MovieDbContext> _options;
        private MovieDbContext _context;
        private FavoriteService _favoriteService;
        private IRepository<Favorite, Guid> _favoriteRepository;
        private IRepository<Movie, Guid> _movieRepository;

        [SetUp]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<MovieDbContext>()
                .UseInMemoryDatabase(databaseName: "MovieCatalogueTestDb")
                .Options;
            _context = new MovieDbContext(_options);

            _favoriteRepository = new BaseRepository<Favorite, Guid>(_context);
            _movieRepository = new BaseRepository<Movie, Guid>(_context);
            _favoriteService = new FavoriteService(_favoriteRepository, _movieRepository);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
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


        [Test]
        public async Task AddToFavoritesAsync_ReturnsFalse_WhenAlreadyFavorite()
        {
            var movie = await CreateMovieAsync("New Movie");
            var userId = Guid.NewGuid();

            var favorite = new Favorite { MovieId = movie.Id, UserId = userId };
            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

            var result = await _favoriteService.AddToFavoritesAsync(movie.Id, userId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task AddToFavoritesAsync_ReturnsTrue_WhenSuccessfullyAdded()
        {
            var movie = await CreateMovieAsync("New Movie");
            var userId = Guid.NewGuid();

            var result = await _favoriteService.AddToFavoritesAsync(movie.Id, userId);

            Assert.IsTrue(result); 
        }

        [Test]
        public async Task RemoveFavoriteAsync_ReturnsTrue_WhenFavoriteExists()
        {
            var movie = await CreateMovieAsync("Test Movie");
            var userId = Guid.NewGuid();

            var favorite = new Favorite { MovieId = movie.Id, UserId = userId };
            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

            var result = await _favoriteService.RemoveFavoriteAsync(movie.Id, userId);

            Assert.IsTrue(result); 
        }

        [Test]
        public async Task RemoveFavoriteAsync_ReturnsFalse_WhenFavoriteDoesNotExist()
        {
            var movie = await CreateMovieAsync("Test Movie");
            var userId = Guid.NewGuid();

            var result = await _favoriteService.RemoveFavoriteAsync(movie.Id, userId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetUserFavoritesByPageAsync_ReturnsFavorites_WhenValidPage()
        {
            var userId = Guid.NewGuid();
            var movie1 = await CreateMovieAsync("Movie 1");
            var movie2 = await CreateMovieAsync("Movie 2");

            _context.Favorites.AddRange(
                new Favorite { MovieId = movie1.Id, UserId = userId },
                new Favorite { MovieId = movie2.Id, UserId = userId }
            );
            await _context.SaveChangesAsync();

            var result = await _favoriteService.GetUserFavoritesByPageAsync(userId, 1, 2);

            Assert.AreEqual(2, result.Count()); 
        }

        [Test]
        public async Task GetTotalFavoritesForUserAsync_ReturnsCorrectCount()
        {
            var userId = Guid.NewGuid();
            var movie1 = await CreateMovieAsync("Movie 1");
            var movie2 = await CreateMovieAsync("Movie 2");

            _context.Favorites.AddRange(
                new Favorite { MovieId = movie1.Id, UserId = userId },
                new Favorite { MovieId = movie2.Id, UserId = userId }
            );
            await _context.SaveChangesAsync();

            var result = await _favoriteService.GetTotalFavoritesForUserAsync(userId);

            Assert.AreEqual(2, result);
        }
    }
}
