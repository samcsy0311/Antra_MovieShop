using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.API.Controllers
{
     [Route("api/[controller]")]
     [ApiController]
     public class CastController : ControllerBase
     {
          private readonly ICastService _castService;
          public CastController(ICastService castService)
          {
               _castService = castService;
          }

          [HttpGet]
          [Route("{id:int}")]
          public async Task<IActionResult> GetCast(int id)
          {
               var cast = await _castService.GetCast(id);
               if (cast == null) return NotFound();
               return Ok(cast);
          }
     }
}
