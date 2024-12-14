using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Data.Repository.Interfaces;
using MovieCatalogue.Services.Data.Interfaces;
using MovieCatalogue.Web.ViewModels.Favorite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Services.Data
{
    public class FavoriteService : BaseService, IFavoriteService
    {
        private readonly IRepository<Movie, Guid> _movieRepository;
        private readonly IRepository<Favorite, Guid> _favoriteRepository;

        public FavoriteService(IRepository<Favorite, Guid> favoriteRepository, IRepository<Movie, Guid> movieRepository)
        {
            _favoriteRepository = favoriteRepository;
            _movieRepository = movieRepository;
        }

        public async Task<int> GetTotalFavoritesForUserAsync(Guid userId)
        {
            return await _favoriteRepository
                .GetAllAttached()
                .Where(f => f.UserId == userId && f.IsDeleted == false)
                .CountAsync();
        }

        public async Task<bool> AddToFavoritesAsync(Guid movieId, Guid userId)
        {
            var exists = await _favoriteRepository
           .GetAllAttached()
           .AnyAsync(f => f.MovieId == movieId && f.UserId == userId);

            if (exists)
            {
                return false;
            }
            var favorite = new Favorite
            {
                MovieId = movieId,
                UserId = userId
            };

            await _favoriteRepository.AddAsync(favorite);

            return true;
        }
        public async Task<IEnumerable<AddMovieToFavorite>> GetUserFavoritesByPageAsync(Guid userId, int page, int pageSize)
        {
            return await _favoriteRepository
                .GetAllWithInclude(f => f.Movie)
                .Include(f => f.Movie.Genre)
                .Where(f => f.UserId == userId && f.IsDeleted == false)
                .OrderBy(f => f.Movie.Title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(f => new AddMovieToFavorite
                {
                    FavoriteId = f.Id,
                    MovieId = f.Movie.Id,
                    MovieTitle = f.Movie.Title,
                    Genre = f.Movie.Genre.Name,
                    DirectorName = f.Movie.Director,
                    PosterUrl = f.Movie.PosterUrl,
                    MovieDescription = f.Movie.Description,
                    IsFavorite = true
                })
                .ToListAsync();
        }

        public async Task<RemoveMovieFromFavorite?> GetFavoriteByMovieIdAsync(Guid movieId, Guid userId)
        {
            var favorite = await _favoriteRepository
           .GetAllWithInclude(f => f.Movie)
           .Where(f => f.MovieId == movieId && f.UserId == userId)
           .Select(f => new RemoveMovieFromFavorite
           {
               MovieId = f.MovieId,
               MovieTitle = f.Movie.Title
           })
           .FirstOrDefaultAsync();

            return favorite;
        }

        public async Task<bool> RemoveFavoriteAsync(Guid movieId, Guid userId)
        {
            var favorite = await _favoriteRepository
            .GetAllWithInclude(f => f.Movie)
            .FirstOrDefaultAsync(f => f.MovieId == movieId && f.UserId == userId);

            if (favorite == null)
            {
                return false;
            }

            _favoriteRepository.Delete(favorite);

            return true;
        }

    }
}