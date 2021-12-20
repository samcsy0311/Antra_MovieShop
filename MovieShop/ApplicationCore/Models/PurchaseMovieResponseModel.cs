using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class PurchaseMovieResponseModel
    {
          public PurchaseMovieResponseModel()
          {
               Purchases = new PurchaseResponseModel();
          }
          public int Id { get; set; }
          public string? Title { get; set; }
          public string? PosterUrl { get; set; }
          public decimal? Price { get; set; }
          public PurchaseResponseModel Purchases { get; set; }
     }
}
