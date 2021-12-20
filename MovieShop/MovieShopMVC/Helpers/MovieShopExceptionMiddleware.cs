using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieShopMVC.Helpers
{
     // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
     public class MovieShopExceptionMiddleware
     {
          private readonly RequestDelegate _next;
          private readonly ILogger<MovieShopExceptionMiddleware> _logger;

          public MovieShopExceptionMiddleware(RequestDelegate next, ILogger<MovieShopExceptionMiddleware> logger)
          {
               _next = next;
               _logger = logger;
          }

          public async Task Invoke(HttpContext httpContext)
          {
               _logger.LogInformation("Inside Movieshop Exception Middleware");

               try
               {
                    await _next(httpContext);
               }
               catch(Exception ex)
               {
                    // exception happens, so handle the exception and implement the logging
                    _logger.LogInformation("-------------- START EXCEPTION ----------------");
                    await HandleException(httpContext, ex);
               }
          }

          private async Task HandleException(HttpContext context, Exception ex)
          {
               _logger.LogError("Something went wrong {0}", ex.Message);

               var errorDetails = new
               {
                    ExceptionMessage = ex.Message,
                    DateOccured = DateTime.UtcNow,
                    StackTrace = ex.StackTrace,
                    InnerException = ex.InnerException,
                    URL = context.Request.Path,
                    IsAuthenticated = context.User.Identity.IsAuthenticated,
                    UserId = Convert.ToInt32(context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
               };

               // we are gonna use Serilog to log above object to either JSON or Text files
               _logger.LogInformation("{@errorDetails}", errorDetails);

               _logger.LogInformation("-------------- END EXCEPTION ------------------");
               context.Response.Redirect("/Home/Error");
               await Task.CompletedTask;
          }
     }

     // Extension method used to add the middleware to the HTTP request pipeline.
     public static class MovieShopExceptionMiddlewareExtensions
     {
          public static IApplicationBuilder UseMovieShopExceptionMiddleware(this IApplicationBuilder builder)
          {
               return builder.UseMiddleware<MovieShopExceptionMiddleware>();
          }
     }
}
