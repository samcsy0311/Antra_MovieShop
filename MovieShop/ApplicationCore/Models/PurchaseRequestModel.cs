using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
     public class PurchaseRequestModel
     {
          public PurchaseRequestModel()
          {
               
          }
          public int userId { get; set; }
          public Guid? purchaseNumber { get; set; }
          public decimal? totalPrice { get; set; }
          public DateTime? purchaseDateTime { get; set; }
          public int movieId { get; set; }
     }
}
