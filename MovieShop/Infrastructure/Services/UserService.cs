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
     public class UserService : IUserService
     {
          private readonly IUserRepository _userRepository;
          public UserService (IUserRepository userRepository)
          {
               _userRepository = userRepository;
          }
          public async Task<bool> EditUserProfile(UserDetailsModel user)
          {
               var dbUser = _userRepository.GetUserByEmail(user.Email);
               if (dbUser != null)
               {
                    throw new Exception("Email already exists and please check!");
                    return false;
               }

               if (user.FirstName == null && user.LastName == null && user.DateOfBirth == null 
                    && user.Email == null && user.PhoneNumber == null)
               {
                    return false;
               }

               var _user = new User
               {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    DateOfBirth = user.DateOfBirth
               };
               _user = await _userRepository.UpdateUser(_user);
               return true;
          }

          public async Task<UserDetailsModel> GetUserDetails(int id)
          {
               var user = await _userRepository.GetById(id);

               var userDetails = new UserDetailsModel
               {
                    Id = id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
               };
               return userDetails;
          }

          public async Task<User> GetUser (int id)
          {
               var user = await _userRepository.GetById(id);
               return user;
          }

          public async Task<IEnumerable<User>> GetAllUsers()
          {
               var users = await _userRepository.GetAll();
               return users;
          }

          public async Task<List<MovieCardResponseModel>> GetUserFavoritedMovies(int id)
          {
               var movie = await _userRepository.GetFavoritedMovies(id);

               var movieCards = new List<MovieCardResponseModel>();

               foreach (var item in movie)
               {
                    movieCards.Add(new MovieCardResponseModel
                    { Id = item.Id, PosterUrl = item.PosterUrl, Title = item.Title });
               }

               return movieCards;
          }

          public async Task<List<PurchaseMovieResponseModel>> GetUserPurchasedMovies(int id)
          {
               var movie = await _userRepository.GetPurchasedMovies(id);

               var movieCards = new List<PurchaseMovieResponseModel>();

               foreach(var item in movie)
               {
                    var purchase = item.Purchases[0];
                    movieCards.Add(new PurchaseMovieResponseModel
                    {
                         Id = item.Id,
                         PosterUrl = item.PosterUrl,
                         Title = item.Title,
                         Price = item.Price,
                         Purchases = new PurchaseResponseModel
                         {
                              Id = purchase.Id,
                              PurchaseNumber = purchase.PurchaseNumber,
                              PurchaseDateTime = purchase.PurchaseDateTime
                         }
                    });
               }

               return movieCards;
          }

          public async Task<Movie> UserHasFavoritedMovie(int id, int movieId)
          {
               var movie = await _userRepository.GetFavoriteByMovieId(id, movieId);
               return movie;
          }
     }
}
