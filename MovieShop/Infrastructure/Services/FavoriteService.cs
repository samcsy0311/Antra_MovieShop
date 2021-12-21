using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
     public class FavoriteService : IFavoriteService
     {
          private readonly IFavoriteRepository _favoriteRepository;

          public FavoriteService(IFavoriteRepository favoriteRepository)
          {
               _favoriteRepository = favoriteRepository;
          }

          public async Task<int> AddFavorite(FavoriteRequestModel favoriteRequestModel)
          {
               var favorite = await _favoriteRepository.GetFavoriteMovie(favoriteRequestModel.movieId, favoriteRequestModel.userId);
               if (favorite != null) return -1;

               var newFavorite = new Favorite
               {
                    MovieId = favoriteRequestModel.movieId,
                    UserId = favoriteRequestModel.userId
               };
               newFavorite = await _favoriteRepository.Add(newFavorite);
               return newFavorite.Id;
          }

          public async Task<Favorite> GetFavorite(int id)
          {
               var favorite = await _favoriteRepository.GetById(id);
               return favorite;
          }

          public async Task<Favorite> GetFavoriteByUserMovieId (int userId, int movieId)
          {
               var favorite = await _favoriteRepository.GetFavoriteMovie(movieId, userId);
               return favorite;
          }

          public async Task<bool> Unfavorite (int id)
          {
               var favorite = await _favoriteRepository.Delete(id);
               if (favorite != null) return true;
               return false;
          } 
     }
}
