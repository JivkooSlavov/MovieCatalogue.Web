using MovieCatalogue.Web.ViewModels.Favorite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Services.Data.Interfaces
{
    public interface IFavoriteService
    {
        Task<IEnumerable<AddMovieToFavorite>> GetUserFavoritesAsync(Guid userId);

        Task<bool> AddToFavoritesAsync(Guid movieId, Guid userId);
        Task<RemoveMovieFromFavorite?> GetFavoriteByMovieIdAsync(Guid movieId, Guid userId);
        Task<bool> RemoveFavoriteAsync(Guid movieId, Guid userId);
    }
}