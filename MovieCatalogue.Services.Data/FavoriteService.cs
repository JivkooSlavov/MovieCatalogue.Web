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
        public async Task<IEnumerable<AddMovieToFavorite>> GetUserFavoritesAsync(Guid userId)
        {
            return await _favoriteRepository
            .GetAllWithInclude(f => f.Movie)
            .Include(f=>f.Movie.Genre)
            .Where(f => f.UserId == userId)
            .Select(f => new AddMovieToFavorite
            {
                FavoriteId = f.Id,
                DirectorName = f.Movie.Director,
                MovieId = f.Movie.Id,
                Genre = f.Movie.Genre.Name,
                MovieTitle = f.Movie.Title,
                MovieDescription = f.Movie.Description,
                PosterUrl = f.Movie.PosterUrl,
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