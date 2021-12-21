using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
     public interface IReviewRepository : IRepository<Review>
     {
          Task<IEnumerable<Review>> GetByMovieId(int id);
          Task<IEnumerable<Review>> GetByUserId(int id);
     }
}
