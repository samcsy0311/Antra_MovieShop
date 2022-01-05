using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.API.Controllers
{
     [Route("api/[controller]")]
     [ApiController]
     public class UserController : ControllerBase
     {
          private readonly IUserService _userService;
          private readonly IReviewService _reviewService;
          private readonly IPurchaseService _purchaseService;
          private readonly IFavoriteService _favoriteService;

          public UserController(IUserService userService, IReviewService reviewService, 
               IPurchaseService purchaseService, IFavoriteService favoriteService)
          {
               _userService = userService;
               _reviewService = reviewService;
               _purchaseService = purchaseService;
               _favoriteService = favoriteService;
          }

          [HttpPost]
          [Route("purchase")]
          public async Task<IActionResult> PurchaseMovie([FromBody] PurchaseRequestModel purchaseRequestModel)
          {
               int newId = await _purchaseService.AddPurchases(purchaseRequestModel);
               if (newId == -1) return BadRequest("Movie already purchased");
               var newPurchase = await _purchaseService.GetPurchases(newId);
               return Ok(newPurchase);
          }

          [HttpPost]
          [Route("favorite")]
          public async Task<IActionResult> FavoriteMovie([FromBody] FavoriteRequestModel favoriteRequestModel)
          {
               int newId = await _favoriteService.AddFavorite(favoriteRequestModel);
               if (newId == -1) return BadRequest("Movie already favorited");
               var newFavorite = await _favoriteService.GetFavorite(newId);
               return Ok(newFavorite);
          }

          [HttpPost]
          [Route("unfavorite")]
          public async Task<IActionResult> UnfavoriteMovie([FromBody] FavoriteRequestModel favoriteRequestModel)
          {
               var favorite = await _favoriteService.GetFavoriteByUserMovieId(favoriteRequestModel.userId, favoriteRequestModel.movieId);
               if (favorite == null) return BadRequest("Movie is not favorited yet");
               var success = await _favoriteService.Unfavorite(favorite.Id);
               return Ok(success);
          }

          [HttpGet]
          [Route("{id:int}/movie/{movieId:int}/favorite")]
          public async Task<IActionResult> IsMovieFavorited(int id, int movieId)
          {
               var movie = await _userService.UserHasFavoritedMovie(id, movieId);
               if (movie == null) return NotFound();
               return Ok(movie);
          }

          [HttpPost]
          [Route("review")]
          public async Task<IActionResult> ReviewMovie(ReviewRequestModel reviewRequestModel)
          {
               var result = await _reviewService.AddReview(reviewRequestModel);
               if (result == -1) return BadRequest("Review already exists");
               var newReview = await _reviewService.getReviewByUserMovieId(reviewRequestModel.userId, reviewRequestModel.movieId);
               return Ok(newReview);
          }

          [HttpPut]
          [Route("review")]
          public async Task<IActionResult> UpdateReviewMovie(ReviewRequestModel reviewRequestModel)
          {
               var result = await _reviewService.UpdateReview(reviewRequestModel);
               if (result == -1) return BadRequest("Review update failed");
               var newReview = await _reviewService.getReviewByUserMovieId(reviewRequestModel.userId, reviewRequestModel.movieId);
               return Ok(newReview);
          }

          [HttpDelete]
          [Route("{userId:int}/movie/{movieId:int}")]
          public async Task<IActionResult> DeleteReview(int userId, int movieId)
          {
               var review = await _reviewService.DeleteReview(userId, movieId);
               if (review == null) return BadRequest("Review does not exist");
               return Ok(review);
          }

          [Authorize]
          [HttpGet]
          [Route("{id:int}/purchases")]
          public async Task<IActionResult> GetPurchases(int id)
          {
               var purchases = await _userService.GetUserPurchasedMovies(id);
               if (purchases == null) return NotFound();
               return Ok(purchases);
          }

          [HttpGet]
          [Route("{id:int}/favorites")]
          public async Task<IActionResult> GetFavorites(int id)
          {
               var favorites = await _userService.GetUserFavoritedMovies(id);
               if (favorites == null) return NotFound();
               return Ok(favorites);
          }

          [HttpGet]
          [Route("{id:int}/reviews")]
          public async Task<IActionResult> GetReviews(int id)
          {
               var reviews = await _reviewService.getReviewsFromUser(id);
               if (reviews == null) return NotFound();
               return Ok(reviews);
          }
     }
}
