using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Data.Repository.Interfaces;
using MovieCatalogue.Services.Data.Interfaces;
using MovieCatalogue.Web.ViewModels.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Services.Data
{
    public class ReviewService : BaseService, IReviewService
    {
        private readonly IRepository<Review, Guid> _reviewRepository;
        private readonly IRepository<Movie, Guid> _movieRepository;
        private readonly IRepository<User, Guid> userReviewRepository;

        public ReviewService(
         IRepository<Review, Guid> reviewRepository,
         IRepository<Movie, Guid> movieRepository,
         IRepository<User, Guid> userReviewRepository)
        {
            _reviewRepository = reviewRepository;
            _movieRepository = movieRepository;
            this.userReviewRepository = userReviewRepository;
        }

        public async Task<ReviewIndexViewModel> GetReviewsForMovieAsync(Guid movieId)
        {
            var movie = await _movieRepository
                .GetAllWithInclude(m => m.Reviews)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null)
            {
                return null;
            }

            User user = userReviewRepository.GetById(movie.Id);

            var model = new ReviewIndexViewModel
            {
                MovieId = movie.Id,
                MovieTitle = movie.Title,
                Reviews = movie.Reviews
                    .OrderByDescending(r => r.DatePosted)
                    .Select(r => new ReviewViewModel
                    {
                        Id = r.Id,
                        Content = r.Content,
                        CreatedAt = r.DatePosted
                    })
                    .ToList()
            };

            return model;
        }

        public async Task<bool> CreateReviewAsync(ReviewCreateViewModel reviewVm, Guid userId)
        {
            var review = new Review
            {
                Content = reviewVm.Content,
                MovieId = reviewVm.MovieId,
                UserId = userId,
                DatePosted = DateTime.UtcNow
            };

            await _reviewRepository.AddAsync(review);
            return true;
        }

        public async Task<ReviewCreateViewModel> GetReviewForEditAsync(Guid id, Guid userId)
        {
            var review = await _reviewRepository.FirstOrDefaultAsync(r => r.Id == id);

            if (review == null || review.UserId != userId)
            {
                return null;
            }

            return new ReviewCreateViewModel
            {
                Id = review.Id,
                MovieId = review.MovieId,
                Content = review.Content,
                UpdatePosted = DateTime.UtcNow
            };
        }

        public async Task<bool> UpdateReviewAsync(ReviewEditViewModel reviewVm)
        {
            var review = await _reviewRepository.FirstOrDefaultAsync(r => r.Id == reviewVm.Id);

            if (review == null)
            {
                return false;
            }

            review.Content = reviewVm.Content;
            review.UpdatePosted = DateTime.UtcNow;

            await _reviewRepository.UpdateAsync(review);
            return true;
        }

        public async Task<ReviewDeleteViewModel> GetReviewForDeleteAsync(Guid id, Guid userId)
        {
            var review = await _reviewRepository.FirstOrDefaultAsync(r => r.Id == id);

            if (review == null || review.UserId != userId)
            {
                return null;
            }

            return new ReviewDeleteViewModel
            {
                Id = review.Id,
                MovieId = review.MovieId,
                MovieTitle = review.Movie.Title,
                Content = review.Content,
                CreatedAt = review.DatePosted,
                CreatedByUserId = review.UserId
            };
        }

        public async Task<bool> DeleteReviewAsync(Guid reviewId, Guid userId)
        {
            var review = await _reviewRepository.FirstOrDefaultAsync(r => r.Id == reviewId);

            if (review == null || review.UserId != userId)
            {
                return false;
            }

            await _reviewRepository.DeleteAsync(review);
            return true;
        }


    }
}
