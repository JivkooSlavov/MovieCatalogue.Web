using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Data.Repository.Interfaces;
using MovieCatalogue.Services.Data.Interfaces;
using MovieCatalogue.Web.ViewModels.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Services.Data
{
    public class RatingService : BaseService, IRatingService
    {
        private readonly IRepository<Movie, Guid> _movieRepository;
        private readonly IRepository<Rating, int> _ratingRepository;

        public RatingService(IRepository<Rating, int> ratingRepository, IRepository<Movie, Guid> movieRepository)
        {
            _ratingRepository = ratingRepository;
            _movieRepository = movieRepository;
        }

        public async Task AddOrUpdateRatingAsync(RatingViewModel model, Guid userId)
        {
            var existingRating = await _ratingRepository.FirstOrDefaultAsync(r => r.MovieId == model.MovieId && r.UserId == userId);

            if (existingRating != null)
            {
                existingRating.Value = model.Value;
                await _ratingRepository.UpdateAsync(existingRating);
            }
            else
            {
                var rating = new Rating
                {
                    MovieId = model.MovieId,
                    UserId = userId,
                    Value = model.Value
                };

                await _ratingRepository.AddAsync(rating);
            }

            await UpdateMovieRatingAsync(model.MovieId);
        }
        public async Task UpdateMovieRatingAsync(Guid movieId)
        {
            var movie = await _movieRepository.GetAllWithInclude(m => m.Ratings)
                                                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie != null)
            {
                movie.Rating = movie.Ratings.Any()
                    ? movie.Ratings.Average(r => r.Value)
                    : 0;

                await _movieRepository.UpdateAsync(movie);
            }
        }
    }
}
