using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
     public class ReviewService : IReviewService
     {
          private readonly IReviewRepository _reviewRepository;

          public ReviewService(IReviewRepository reviewRepository)
          {
               _reviewRepository = reviewRepository;
          }

          public async Task<IEnumerable<Review>> getAllReviews(int id)
          {
               var reviews = await _reviewRepository.GetByMovieId(id);
               return reviews;
          }

          public async Task<IEnumerable<Review>> getReviewsFromUser(int id)
          {
               var reviews = await _reviewRepository.GetByUserId(id);
               return reviews;
          }

          public async Task<Review> getReviewByUserMovieId(int userId, int movieId)
          {
               var review = await _reviewRepository.GetByUserMovieId(userId, movieId);
               return review;
          }

          public async Task<int> AddReview(ReviewRequestModel reviewRequestModel)
          {
               var review = await _reviewRepository.GetByUserMovieId(reviewRequestModel.userId, reviewRequestModel.movieId);
               if (review != null) return -1;

               var newReview = new Review
               {
                    MovieId = reviewRequestModel.movieId,
                    UserId = reviewRequestModel.userId,
                    Rating = reviewRequestModel.rating,
                    ReviewText = reviewRequestModel.reviewText
               };
               newReview = await _reviewRepository.Add(newReview);
               if (newReview != null) return 0;
               return -1;
          }

          public async Task<int> UpdateReview(ReviewRequestModel reviewRequestModel)
          {
               var newReview = new Review
               {
                    MovieId = reviewRequestModel.movieId,
                    UserId = reviewRequestModel.userId,
                    Rating = reviewRequestModel.rating,
                    ReviewText = reviewRequestModel.reviewText
               };
               var review = await _reviewRepository.GetByUserMovieId(reviewRequestModel.userId, reviewRequestModel.movieId);
               if (review != null)
               {
                    await _reviewRepository.Update(newReview);
                    return 0;
               }
               
               newReview = await _reviewRepository.Add(newReview);
               if (newReview != null) return 0;
               return -1;
          }

          public async Task<Review> DeleteReview (int userId, int movieId)
          {
               var review = await _reviewRepository.Delete(userId, movieId);
               return review;
          }
     }
}
