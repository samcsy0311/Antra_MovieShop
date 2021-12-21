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
     public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
     {
          public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
          {
               
          }
          public async Task<Purchase> GetByUserMovieId(int userId, int movieId)
          {
               var purchase = await _dbContext.Purchases.Where(p => p.UserId == userId && p.MovieId == movieId)
                    .FirstOrDefaultAsync();
               return purchase;
          }
     }
}
