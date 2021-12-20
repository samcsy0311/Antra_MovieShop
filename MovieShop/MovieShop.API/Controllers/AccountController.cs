using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.API.Controllers
{
     [Route("api/[controller]")]
     [ApiController]
     public class AccountController : ControllerBase
     {
          private readonly IAccountService _accountService;

          public AccountController(IAccountService accountService)
          {
               _accountService = accountService;
          }

          [HttpPost]
          [Route("login")]

          public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
          {
               var user = await _accountService.ValidateUser(model);
               if (user == null)
               {
                    return Unauthorized ("wrong email/password");
               }
               // JWT AUthnetcion
               return Ok(user);
          }
     }
}
