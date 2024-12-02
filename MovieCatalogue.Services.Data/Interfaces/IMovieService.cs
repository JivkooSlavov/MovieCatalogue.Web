using MovieCatalogue.Web.ViewModels.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Services.Data.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieInfoViewModel>> GetAllMoviesAsync();

        Task<MovieInfoViewModel?> GetMovieDetailsAsync(Guid id);

        Task<bool> AddMovieAsync(AddMovieViewModel inputModel, Guid userId);

        Task<AddMovieViewModel?> GetMovieForEditAsync(Guid id, Guid currentUserId);
        Task<bool> EditMovieAsync(Guid id, AddMovieViewModel model, Guid currentUserId);

        Task<DeleteMovieViewModel?> GetMovieForDeletionAsync(Guid id, Guid currentUserId);
        Task<bool> DeleteMovieAsync(Guid id, Guid currentUserId);

        Task<IEnumerable<TypeOfGenreMovies>> GetGenresAsync();
    }
}
