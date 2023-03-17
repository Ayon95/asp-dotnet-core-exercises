using Microsoft.AspNetCore.Mvc;

namespace BankAppControllers.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return Content("<h1>Welcome to the best bank</h1>", "text/html");
        }
    }
}
