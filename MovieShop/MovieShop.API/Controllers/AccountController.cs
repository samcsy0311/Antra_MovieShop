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
          private readonly IUserService _userService;

          public AccountController(IAccountService accountService, IUserService userService)
          {
               _accountService = accountService;
               _userService = userService;
          }

          [HttpGet]
          [Route("{Id:int}")]
          public async Task<IActionResult> GetUserDetail(int Id)
          {
               var user = await _userService.GetUser(Id);
               if (user == null) return NotFound();
               return Ok(user);
          }

          [HttpPost]
          [Route("")]
          public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestModel registerRequestModel)
          {
               var user = await _accountService.RegisterUser(registerRequestModel);

               if (user == 0)
               {
                    return BadRequest("User already exists");
               }
               return Ok(user);
          }

          [HttpGet]
          [Route("")]
          public async Task<IActionResult> GetAllUsers ()
          {
               var users = await _userService.GetAllUsers();
               if (!users.Any())
               {
                    return NotFound();
               }
               return Ok(users);
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
