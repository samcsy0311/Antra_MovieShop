using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.API.Controllers
{
     [Route("api/[controller]")]
     [ApiController]
     public class MoviesController : ControllerBase
     {
          private readonly IMovieService _movieService;

          public MoviesController(IMovieService movieService)
          {
               _movieService = movieService;
          }

          [HttpGet]
          [Route("toprevenue")]
          public async Task<IActionResult> GetTopRevenueMovies()
          {
               var movies = await _movieService.GetHighestGrossingMovies();
               if (!movies.Any())
               {
                    return NotFound();
               }
               return Ok(movies);
          }

          [HttpGet]
          [Route("{id:int}")]
          public async Task<IActionResult> Details(int id)
          {
               var movie = await _movieService.GetMovieDetailsById(id);
               if (movie == null) return NotFound();
               return Ok(movie);
          }
     }
}
