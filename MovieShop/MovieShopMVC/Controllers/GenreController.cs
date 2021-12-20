using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
     public class GenreController : Controller
     {
          private readonly IGenreService _genreService;

          public GenreController(IGenreService genreService)
          {
               _genreService = genreService;
          }

          //[HttpGet]
          //public async Task<IActionResult> Genres()
          //{
          //     var genre = await _genreService.GetAllGenres();
          //     return View(genre);
          //}

          [HttpGet]
          public async Task<IActionResult> List(int id)
          {
               var movieDetails = await _genreService.GetMovieOfGenre(id);
               return View(movieDetails);
          }
     }
}
