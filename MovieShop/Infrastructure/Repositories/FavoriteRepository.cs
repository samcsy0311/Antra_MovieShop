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
     public class FavoriteRepository : Repository<Favorite>, IFavoriteRepository
     {
          public FavoriteRepository(MovieShopDbContext dbContext) : base(dbContext)
          {
          }

          public async Task<Favorite> GetFavoriteMovie(int movieId, int userId)
          {
               var favorite = await _dbContext.Favorites.Where(f => f.MovieId == movieId && f.UserId == userId)
                    .FirstOrDefaultAsync();
               return favorite;
          }
     }
}
