﻿using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data.Models;
using MovieCatalogue.Data.Repository.Interfaces;
using MovieCatalogue.Services.Data.Interfaces;
using MovieCatalogue.Web.ViewModels.Movie;
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

        public ReviewService(
         IRepository<Review, Guid> reviewRepository,
         IRepository<Movie, Guid> movieRepository)
        {
            _reviewRepository = reviewRepository;
            _movieRepository = movieRepository;
        }

        public async Task<ReviewIndexViewModel> GetReviewsForMovieAsync(Guid movieId)
        {
            var movie = await _movieRepository
                .GetAllWithInclude(m => m.Genre)
                .Include(m => m.Reviews.Where(r => !r.IsDeleted))
                .ThenInclude(r => r.User)
                .Include(m => m.Ratings)
                .FirstOrDefaultAsync(m => m.Id == movieId && !m.IsDeleted);

            if (movie == null)
            {
                return null;
            }

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
                        CreatedAt = r.DatePosted,
                        UserName = r.User?.UserName,
                        UpdatedAt = r.UpdatePosted
                    })
                    .ToList()
            };

            return model;
        }

        public async Task<bool> CreateReviewAsync(ReviewCreateViewModel reviewVm, Guid userId)
        {
            Review review = new Review
            {
                Content = reviewVm.Content,
                MovieId = reviewVm.MovieId,
                UserId = userId,
                DatePosted = DateTime.Now
            };

            await _reviewRepository.AddAsync(review);
            return true;
        }

        public async Task<ReviewEditViewModel> GetReviewForEditAsync(Guid id, Guid userId, bool isAdmin)
        {
            var reviewForEdit = await _reviewRepository.FirstOrDefaultAsync(r => r.Id == id);

            if (reviewForEdit == null || !isAdmin && reviewForEdit.UserId != userId)
            {
                return null;
            }

            return new ReviewEditViewModel
            {
                Id = reviewForEdit.Id,
                MovieId = reviewForEdit.MovieId,
                Content = reviewForEdit.Content,
                CreatedByUserId = userId,
                DatePosted = reviewForEdit.DatePosted.ToString()
            };
        }

        public async Task<bool> UpdateReviewAsync(ReviewEditViewModel reviewVm, Guid userId, bool isAdmin)
        {
            var reviewUpdated = await _reviewRepository.FirstOrDefaultAsync(r => r.Id == reviewVm.Id);

            if (reviewUpdated == null || !isAdmin && reviewUpdated.UserId != userId)
            {
                return false;
            }

            reviewUpdated.Content = reviewVm.Content;
            reviewUpdated.UpdatePosted = DateTime.Now;

            await _reviewRepository.UpdateAsync(reviewUpdated);
            return true;
        }

        public async Task<ReviewDeleteViewModel> GetReviewForDeleteAsync(Guid id, Guid userId, bool isAdmin)
        {
            var reviewForDelete = await _reviewRepository
                 .GetAllWithInclude(r => r.Movie)
                 .Where(r => r.Id == id)
                 .Select(r => new ReviewDeleteViewModel
                 {
                     Id = r.Id,
                     MovieId = r.MovieId,
                     Content = r.Content,
                     UserName = r.User.UserName,
                     CreatedAt = r.DatePosted,
                     UpdatedAt = r.UpdatePosted,
                     MovieTitle = r.Movie.Title,
                     CreatedByUserId = r.UserId

                 })
                 .FirstOrDefaultAsync();

            if (reviewForDelete == null || !isAdmin && reviewForDelete.CreatedByUserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this review.");
            }

            return reviewForDelete;
        }

        public async Task<bool> DeleteReviewAsync(Guid reviewId, Guid userId, bool isAdmin)
        {
            var reviewDeleted = await _reviewRepository.FirstOrDefaultAsync(r => r.Id == reviewId);

            if (reviewDeleted == null || !isAdmin && reviewDeleted.UserId != userId)
            {
                return false;
            }

            reviewDeleted.IsDeleted = true;
            await _reviewRepository.UpdateAsync(reviewDeleted);

            return true;
        }

        public async Task<int> GetTotalReviewsForUserAsync(Guid userId)
        {
            return await  _reviewRepository
                .GetAllAttached()
                .Where(r => r.UserId == userId && r.IsDeleted == false)
                .CountAsync();
        }

        public async Task<int> GetTotalReviewsAsync()
        {
            return await _reviewRepository
                .GetAllAttached()
                .Where(r=>r.IsDeleted == false)
                .CountAsync();
        }

        public async Task<IEnumerable<UserReviewViewModel>> GetUserReviewsByPageAsync(Guid userId, int pageNumber, int pageSize)
        {
            return await _reviewRepository
                .GetAllWithInclude(r => r.Movie)
                .Where(r => r.UserId == userId && r.IsDeleted == false)
                .OrderByDescending(r => r.DatePosted)
                .Skip((pageNumber - 1) * pageSize) 
                .Take(pageSize) 
                .Select(r => new UserReviewViewModel
                {
                    Id = r.Id,
                    Content = r.Content,
                    CreatedAt = r.DatePosted,
                    MovieTitle = r.Movie.Title,
                    MovieId = r.Movie.Id,
                    UpdatedAt = r.UpdatePosted
                })
                .ToListAsync();
        }


        public async Task<IEnumerable<ReviewViewModel>> GetAllReviewsByPageAsync(int page, int pageSize)
        {
            return await _reviewRepository
                .GetAllWithInclude(r => r.Movie)
                .Include(r => r.User)
                .Where(r => !r.IsDeleted)
                .OrderByDescending(r => r.DatePosted)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new ReviewViewModel
                {
                    Id = r.Id,
                    MovieId = r.MovieId,
                    Content = r.Content,
                    UserName = r.User.UserName,
                    CreatedAt = r.DatePosted,
                    UpdatedAt = r.UpdatePosted,
                    MovieTitle = r.Movie.Title,
                    IsDeleted = r.IsDeleted
                })
                .ToListAsync();
        }
    }
}
