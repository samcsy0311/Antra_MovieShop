using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
     public interface IReviewService
     {
          Task<IEnumerable<Review>> getAllReviews(int id);
          Task<IEnumerable<ReviewResponseModel>> getReviewsFromUser(int id);
          Task<Review> getReviewByUserMovieId(int userId, int movieId);
          Task<int> AddReview(ReviewRequestModel reviewRequestModel);
          Task<int> UpdateReview(ReviewRequestModel reviewRequestModel);
          Task<Review> DeleteReview(int userId, int movieId);
     }
}
