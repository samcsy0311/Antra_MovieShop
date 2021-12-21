using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
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
