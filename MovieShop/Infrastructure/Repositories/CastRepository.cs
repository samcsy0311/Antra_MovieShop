using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
     public class CastRepository : Repository<Cast>, ICastRepository
     {
          public CastRepository(MovieShopDbContext dbContext) : base(dbContext)
          {
          }
     }
}
