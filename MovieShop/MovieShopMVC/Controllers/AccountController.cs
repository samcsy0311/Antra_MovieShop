using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
     public class AccountController : Controller
     {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
               _accountService = accountService;
        }

        // account/register
        [HttpGet]
          public async Task<IActionResult> Register()
          {
               return View();
          }

          [HttpPost]
          public async Task<IActionResult> Register(UserRegisterRequestModel registerRequestModel)
          {
               // we need to send the data to service , which is gonna convert in to User entity and send it to User Repository
               // save the data in the User table

               var user = await _accountService.RegisterUser(registerRequestModel);

               if (user == 0)
               {
                    // email already exist
                    return View();
               }
               return RedirectToAction("Login");
          }

          [HttpGet]
          public async Task<IActionResult> Login()
          {
               return View();
          }

          [HttpPost]
          public async Task<IActionResult> Login(LoginRequestModel loginRequestModel)
          {
               var user = await _accountService.ValidateUser(loginRequestModel);
               if(user == null)
               {
                    // hey please check your email
                    // send message to the view saying please enter correct email/password
               }

               // we need to create a cookie => will have information Claims (MovieShopAuthCookie)
               // claims will have (FirstName, LastName, TimeZone)
               // We usually encrypt the data we store in cookies
               // Cookie Based Authentication
               // Cookie will have expiration time
               // Cookie => Browser 

               //redirect to homepage

               // create claims that we are going to store in the cookie
               var claims = new List<Claim>
               {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.GetValueOrDefault().ToString()),
                    new Claim("Language", "English")
               };

               // Identity object that is going to store the claims and tell it to store those inside the cookie
               var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

               // create the cookie
               // ASP.NET (both core and old asp.net) we have one very very important class called HttpContext
               // HttpContext captures everything about http request
               // what kind of http method GET/POST/PUT, URL, FORM, Cookies, Headers

               // create the cookie
               await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                    new ClaimsPrincipal(claimIdentity));
               
               // redirect to my homepage
               return LocalRedirect("~/");
          }

          [HttpGet]
          public async Task<IActionResult> Logout()
          {
               // invalidate the cookie
               await HttpContext.SignOutAsync();
               return RedirectToAction("Login");
          }
     }
}
