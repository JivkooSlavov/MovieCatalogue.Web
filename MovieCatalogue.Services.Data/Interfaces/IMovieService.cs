using MovieCatalogue.Data.Models;
using MovieCatalogue.Web.ViewModels.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Services.Data.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieInfoViewModel>> GetMoviesByPageAsync(int page, int pageSize);
        Task<MovieInfoViewModel?> GetMovieDetailsAsync(Guid id);

        Task<bool> AddMovieAsync(AddMovieViewModel inputModel, Guid userId);

        Task<AddMovieViewModel?> GetMovieForEditAsync(Guid id, Guid currentUserId, bool isAdmin);
        Task<bool> EditMovieAsync(Guid id, AddMovieViewModel model, Guid currentUserId, bool isAdmin);

        Task<DeleteMovieViewModel?> GetMovieForDeletionAsync(Guid id, Guid currentUserId, bool isAdmin);
        Task<bool> DeleteMovieAsync(Guid id, Guid currentUserId, bool isAdmin);

        Task<IEnumerable<TypeOfGenreMovies>> GetGenresAsync();

        Task<IEnumerable<MovieInfoViewModel>> GetPopularMoviesAsync();
        Task<int> GetTotalMoviesAsync();

    }
}
