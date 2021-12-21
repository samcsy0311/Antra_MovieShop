using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
     public class ReviewRepository : Repository<Review>, IReviewRepository
     {
          public ReviewRepository(MovieShopDbContext dbContext) : base(dbContext)
          {

          }

          public async Task<IEnumerable<Review>> GetByMovieId(int id)
          {
               var reviews = await _dbContext.Reviews.Where(r => r.MovieId == id)
                    .ToListAsync();

               return reviews;
          }

          public async Task<IEnumerable<Review>> GetByUserId(int id)
          {
               var reviews = await _dbContext.Reviews.Where(r => r.UserId == id)
                    .ToListAsync();

               return reviews;
          }

          public async Task<Review> GetByUserMovieId (int userId, int movieId)
          {
               var review = await _dbContext.Reviews.Where(r => r.UserId == userId && r.MovieId == movieId)
                    .FirstOrDefaultAsync();
               return review;
          }

          public async override Task<Review> Update(Review review)
          {
               var _review = await _dbContext.Reviews
                    .Where(r => r.UserId == review.UserId && r.MovieId == review.MovieId)
                    .FirstOrDefaultAsync();
               
               _review.Rating = review.Rating;
               if (review.ReviewText != null)
               {
                    _review.ReviewText = review.ReviewText;
               }

               await _dbContext.SaveChangesAsync();
               return _review;
          }

          public async Task<Review> Delete(int UserId, int MovieId)
          {
               var review = await _dbContext.Reviews.Where(r => r.UserId == UserId && r.MovieId == MovieId)
                    .FirstOrDefaultAsync();
               if (review != null)
               {
                    _dbContext.Reviews.Remove(review);
                    await _dbContext.SaveChangesAsync();
               }
               return review;
          }
     }
}
