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
     }
}
