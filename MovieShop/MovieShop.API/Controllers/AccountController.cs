using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace MovieShop.API.Controllers
{
     [Route("api/[controller]")]
     [ApiController]
     public class AccountController : ControllerBase
     {
          private readonly IAccountService _accountService;
          private readonly IUserService _userService;
          private readonly IConfiguration _configuration;

          public AccountController(IAccountService accountService, IUserService userService, IConfiguration configuration)
          {
               _accountService = accountService;
               _userService = userService;
               _configuration = configuration;
          }

          [HttpGet]
          [Route("{Id:int}")]
          public async Task<IActionResult> GetUserDetail(int Id)
          {
               var user = await _userService.GetUserDetails(Id);
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
               // JWT Authnetcion
               return Ok(new { token = GenerateJWT(user) });
          }

          private string GenerateJWT(UserLoginResponseModel user)
          {
               var claims = new List<Claim>
               {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                    new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName)
               };

               // add the required claims to identity object 
               var identityClaims = new ClaimsIdentity();
               identityClaims.AddClaims(claims);

               // Microsoft.IdentityModel.Tokens
               // get the secret key for signing the tokens
               var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["secretKey"]));

               // specify the algorithm to sign the token
               var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
               var expires = DateTime.UtcNow.AddHours(_configuration.GetValue<int>("ExpirationHours"));

               // creating the token System.IdentityModel.Tokens.Jwt
               var tokenHandler = new JwtSecurityTokenHandler();
               var tokenDescriptor = new SecurityTokenDescriptor
               {
                    Subject = identityClaims,
                    Expires = expires,
                    SigningCredentials = credentials,
                    Issuer = _configuration["Issuer"],
                    Audience = _configuration["Audience"]
               };

               var encodedJwt = tokenHandler.CreateToken(tokenDescriptor);

               return tokenHandler.WriteToken(encodedJwt);
          }
     }
}
