using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
     public class AdminController : Controller
     {
          public async Task<IActionResult> Index()
          {
               return View();
          }
     }
}
