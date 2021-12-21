using ApplicationCore.Entities;
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
     }
}
