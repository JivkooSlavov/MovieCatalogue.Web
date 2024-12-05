using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Data.Repository.Interfaces;
using MovieCatalogue.Services.Data.Interfaces;
using MovieCatalogue.Services.Mapping;
using MovieCatalogue.Web.ViewModels.Movie;
using MovieCatalogue.Web.ViewModels.Review;
using System.Globalization;
using System.Security.Claims;
using static MovieCatalogue.Common.EntityValidationConstants.MovieConstants;

namespace MovieCatalogue.Services.Data
{
    public class MovieService : BaseService, IMovieService
    {
        private readonly IRepository<Movie, Guid> _movieRepository;
        private readonly IRepository<Genre, Guid> _genreRepository;
        private readonly IRepository<Rating, Guid> _ratingRepository;

        public MovieService(IRepository<Movie,Guid> movieRepository, IRepository<Genre, Guid> genreRepository, IRepository<Rating, Guid> ratingRepository)
        {
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
            _ratingRepository = ratingRepository;
        }

        public async Task<IEnumerable<MovieInfoViewModel>> GetAllMoviesAsync()
        {

            var model = await _movieRepository
                 .GetAllAttached()
                 .Include(y => y.Genre)
                 .Where(x=>x.IsDeleted==false)
                 .OrderBy(x=>x.Title)
                      .Select(x => new MovieInfoViewModel
                   {
                       Id = x.Id,
                       Cast = x.Cast,
                       Description = x.Description,
                       Director = x.Director,
                       Duration = x.Duration,
                       PosterUrl = x.PosterUrl,
                       Genre = x.Genre.Name,
                       Rating = x.Rating,
                       CreatedByUserId = x.CreatedByUserId.ToString(),
                       ReleaseDate = x.ReleaseDate.ToString(DateFormatOfMovie),
                       Title = x.Title,
                       TrailerUrl = x.TrailerUrl,
                   })
                   .AsNoTracking()
                   .ToListAsync();

            return model;
        }

        public async Task<MovieInfoViewModel?> GetMovieDetailsAsync(Guid id)
        {
            var movie = await _movieRepository
                  .GetAllWithInclude(m => m.Genre)
                  .Include(m => m.Reviews.Where(r => !r.IsDeleted))
                  .ThenInclude(r => r.User)
                  .Include(m => m.Ratings)
                  .FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);

            if (movie == null)
            {
                return null;
            }
            var averageRating = movie.Ratings.Any() ? movie.Ratings.Average(r => r.Value) : 0;

            return new MovieInfoViewModel
            {
                Id = movie.Id,
                Cast = movie.Cast,
                Description = movie.Description,
                Director = movie.Director,
                Duration = movie.Duration,
                PosterUrl = movie.PosterUrl,
                Genre = movie.Genre.Name,
                Rating = averageRating,
                CreatedByUserId = movie.CreatedByUserId.ToString(),
                ReleaseDate = movie.ReleaseDate.ToString(DateFormatOfMovie),
                Title = movie.Title,
                TrailerUrl = movie.TrailerUrl,
                Reviews = movie.Reviews
                    .OrderBy(r => r.DatePosted)
                    .Select(r => new ReviewViewModel
                    {
                        Id = r.Id,
                        Content = r.Content,
                        CreatedAt = r.DatePosted,
                        UserName = r.User.UserName,
                        UpdatedAt = r.UpdatePosted
                    }).ToList()
            };
        }

        public async Task<bool> AddMovieAsync(AddMovieViewModel model, Guid userId)
        {
            bool isReleaseDateValid = DateTime.TryParseExact(
                model.ReleaseDate,
                DateFormatOfMovie,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime releaseDate
            );

            if (!isReleaseDateValid)
            {
                return false;
            }
            Movie movie = new Movie
            {
                Title = model.Title,
                Description = model.Description,
                GenreId = model.GenreId,
                ReleaseDate = releaseDate,
                Cast = model.Cast,
                TrailerUrl = model.TrailerUrl,
                PosterUrl = model.PosterUrl,
                Director = model.Director,
                Duration = model.Duration,
                CreatedByUserId = userId
            };

            Rating creatorRating = new Rating
            {
                Movie = movie,
                UserId = userId,
                Value = (int)model.Rating
            };

            movie.Ratings.Add(creatorRating);

            movie.Rating = movie.Ratings.Average(r => r.Value);

            await _movieRepository.AddAsync(movie);

            return true;
        }

        public async Task<AddMovieViewModel?> GetMovieForEditAsync(Guid id, Guid currentUserId, bool isAdmin)
        {
            var movie = await _movieRepository.GetByIdAsync(id);

            if (movie == null || (!isAdmin && movie.CreatedByUserId != currentUserId))
            {
                return null;
            }

            return new AddMovieViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Cast = movie.Cast,
                Director = movie.Director,
                Duration = movie.Duration,
                Rating = movie.Rating,
                ReleaseDate = movie.ReleaseDate.ToString(DateFormatOfMovie),
                PosterUrl = movie.PosterUrl,
                TrailerUrl = movie.TrailerUrl,
                GenreId = movie.GenreId,
                CreatedByUserId = movie.CreatedByUserId,
                Genres = _genreRepository.GetAllAttached()
                    .Select(g => new TypeOfGenreMovies { Id = g.Id, Name = g.Name })
                    .ToList()
            };
        }


        public async Task<bool> EditMovieAsync(Guid id, AddMovieViewModel model, Guid currentUserId, bool isAdmin)
        {
            var movie = await _movieRepository.GetByIdAsync(id);

            if (movie == null || (!isAdmin && movie.CreatedByUserId != currentUserId))
            {
                return false; 
            }

            bool isReleaseDateValid = DateTime.TryParseExact(
                model.ReleaseDate,
                DateFormatOfMovie,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime releaseDate);

            if (!isReleaseDateValid)
            {
                return false; 
            }

            movie.Title = model.Title;
            movie.Description = model.Description;
            movie.Cast = model.Cast;
            movie.Director = model.Director;
            movie.Duration = model.Duration;
            movie.Rating = model.Rating;
            movie.ReleaseDate = releaseDate;
            movie.PosterUrl = model.PosterUrl;
            movie.TrailerUrl = model.TrailerUrl ?? string.Empty;
            movie.GenreId = model.GenreId;

            await _movieRepository.UpdateAsync(movie);

            return true;
        }


        public async Task<DeleteMovieViewModel?> GetMovieForDeletionAsync(Guid id, Guid currentUserId, bool isAdmin)
        {
            var movie = await _movieRepository.GetAllWithInclude(m => m.Genre)
                .Where(m => m.Id == id && !m.IsDeleted)
                .Select(m => new DeleteMovieViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    GenreName = m.Genre.Name,
                    PosterUrl = m.PosterUrl,
                    Rating = m.Rating,
                    ReleaseDate = m.ReleaseDate,
                    CreatedByUserId = m.CreatedByUserId
                })
                .FirstOrDefaultAsync();

            if (movie == null || (movie.CreatedByUserId != currentUserId && !isAdmin))
            {
                return null;
            }

            return movie;
        }


        public async Task<bool> DeleteMovieAsync(Guid id, Guid currentUserId, bool isAdmin)
        {
            var movie = await _movieRepository.GetByIdAsync(id);

            if (movie == null || (movie.CreatedByUserId != currentUserId && !isAdmin) || movie.IsDeleted)
            {
                return false;
            }

            movie.IsDeleted = true;
            await _movieRepository.UpdateAsync(movie);

            return true;
        }

        public async Task<IEnumerable<MovieInfoViewModel>> GetPopularMoviesAsync()
        {
            return await _movieRepository.GetAllAttached()
                .OrderByDescending(m => m.Rating)
                .Where(m => !m.IsDeleted)
                .Take(4)
                .Select(m => new MovieInfoViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    PosterUrl = m.PosterUrl,
                    Rating = m.Rating,
                    Cast = m.Cast,
                    Description = m.Description,
                    Director = m.Director,
                    Duration = m.Duration,
                    ReleaseDate = m.ReleaseDate.ToString(),
                    TrailerUrl = m.TrailerUrl
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<TypeOfGenreMovies>> GetGenresAsync()
        {

            var genres = await _genreRepository.GetAllAsync(); 

            return genres.Select(g => new TypeOfGenreMovies
            {
                Id = g.Id,
                Name = g.Name
            }).ToList();
        }
    }
}
