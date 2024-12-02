using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Data.Repository.Interfaces;
using MovieCatalogue.Services.Data.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCatalogue.Services.Data
{
    public class RatingService : IRatingService
    {
        private readonly IRepository<Rating, Guid> _ratingRepository;
        private readonly IRepository<Movie, Guid> _movieRepository;

        public RatingService(IRepository<Rating, Guid> ratingRepository, IRepository<Movie, Guid> movieRepository)
        {
            _ratingRepository = ratingRepository;
            _movieRepository = movieRepository;
        }


        public async Task AddOrUpdateRatingAsync(Guid movieId, Guid userId, int value)
        {
            var existingRating = await _ratingRepository.FirstOrDefaultAsync(r => r.MovieId == movieId && r.UserId == userId);

            if (existingRating != null)
            {
                existingRating.Value = value;
                await _ratingRepository.UpdateAsync(existingRating);
            }
            else
            {
                var rating = new Rating
                {
                    MovieId = movieId,
                    UserId = userId,
                    Value = value
                };
                await _ratingRepository.AddAsync(rating);
            }

            await UpdateMovieRatingAsync(movieId);
        }
        public async Task UpdateMovieRatingAsync(Guid movieId)
        {
            var movie = await _movieRepository.GetAllWithInclude(m => m.Ratings)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie != null && movie.Ratings.Any())
            {
                movie.Rating = movie.Ratings.Average(r => r.Value);

                await _movieRepository.UpdateAsync(movie);
            }
            else
            {
                movie.Rating = 0;

                await _movieRepository.UpdateAsync(movie);
            }
        }
    }
}
