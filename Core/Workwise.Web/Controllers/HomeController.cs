using Microsoft.AspNetCore.Mvc;

namespace Workwise.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("home/greet")]
        public IActionResult Greet()
        {
            var greeting = new Greeting { Username = "alameenboss" };
            return View(greeting);
        }
    }
    public class Greeting
    {
        public string Username { get; set; }
    }
}
