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
          private readonly IGenreService _genreService;
          private readonly IReviewService _reviewService;

          public MoviesController(IMovieService movieService, IGenreService genreService, IReviewService reviewService)
          {
               _movieService = movieService;
               _genreService = genreService;
               _reviewService = reviewService;
          }

          [HttpGet]
          [Route("")]
          public async Task<IActionResult> GetMovies()
          {
               var movies = await _movieService.GetAllMovies();
               if (!movies.Any())
               {
                    return NotFound();
               }
               return Ok(movies);
          }

          [HttpGet]
          [Route("toprated")]
          public async Task<IActionResult> GetTopRatedMovies()
          {
               var movies = await _movieService.GetHighestRatedMovies();
               if (!movies.Any())
               {
                    return NotFound();
               }
               return Ok(movies);
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

          [HttpGet]
          [Route("genre/{id:int}")]
          public async Task<IActionResult> MoviesByGenre(int id)
          {
               var movies = await _genreService.GetMovieOfGenre(id);
               if (!movies.Any())
               {
                    return NotFound();
               }
               return Ok(movies);
          }

          [HttpGet]
          [Route("{id:int}/reviews")]
          public async Task<IActionResult> GetReviews(int id)
          {
               var reviews = await _reviewService.getAllReviews(id);
               if (!reviews.Any())
               {
                    return NotFound();
               }
               return Ok(reviews);
          }
     }
}
