using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.API.Controllers
{
     [Route("api/[controller]")]
     [ApiController]
     public class AdminController : ControllerBase
     {
          private readonly IPurchaseService _purchaseService;
          private readonly IMovieService _movieService;

          public AdminController(IPurchaseService purchaseService, IMovieService movieService)
          {
               _purchaseService = purchaseService;
               _movieService = movieService;
          }

          [HttpPost]
          [Route("movie")]
          public async Task<IActionResult> AddMovie([FromBody] MovieCreateRequest movieCreateRequest)
          {
               var newId = await _movieService.AddMovie(movieCreateRequest);
               if (newId == -1) return BadRequest("Cannot add movie / Movie already exists");
               var movie = await _movieService.GetMovieDetailsById(newId);
               return Ok(movie);
          }

          [HttpPut]
          [Route("movie")]
          public async Task<IActionResult> UpdateMovie([FromBody] MovieCreateRequest movieCreateRequest)
          {
               var newId = await _movieService.UpdateMovie(movieCreateRequest);
               if (newId == -1) return BadRequest("Cannot add movie / Movie already exists");
               var movie = await _movieService.GetMovieDetailsById(newId);
               return Ok(movie);
          }

          [HttpGet]
          [Route("purchases")]
          public async Task<IActionResult> GetPurchases()
          {
               var purchases = await _purchaseService.GetAllPurchases();
               if (!purchases.Any())
               {
                    return NotFound();
               }
               return Ok(purchases);
          }
     }
}
