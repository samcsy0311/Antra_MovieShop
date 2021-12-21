using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
     public interface IFavoriteService
     {
          Task<int> AddFavorite(FavoriteRequestModel favoriteRequestModel);
          Task<Favorite> GetFavorite(int id);
          Task<Favorite> GetFavoriteByUserMovieId(int userId, int movieId);
          Task<bool> Unfavorite(int id);
     }
}
