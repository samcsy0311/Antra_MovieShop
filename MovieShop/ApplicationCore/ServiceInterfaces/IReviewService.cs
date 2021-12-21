using ApplicationCore.Entities;
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
          Task<IEnumerable<Review>> getReviewsFromUser(int id);
     }
}
