using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
          Task<List<PurchaseMovieResponseModel>> GetUserPurchasedMovies(int id);
          Task<List<MovieCardResponseModel>> GetUserFavoritedMovies(int id);
          Task<UserDetailsModel> GetUserDetails(int id);
          Task<User> GetUser(int id);
          Task<IEnumerable<User>> GetAllUsers();
          Task<bool> EditUserProfile(UserDetailsModel userDetailsModel);
          Task<Movie> UserHasFavoritedMovie(int id, int movieId);
    }
}
