using Microsoft.AspNetCore.Mvc;

namespace House_Renting_System.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error(int statusCode)
        {
            if (statusCode == 401)
            {
                return View("Error401");
            }
            else
            {
                return View("Error404");
            }
        }

        public IActionResult ServerError()
        {
            Response.StatusCode = 500;
            return View("Error");
        }
    }
}