using Microsoft.AspNetCore.Mvc;

namespace ISPH.API.Controllers.ViewControllers
{
    [Controller]
    public class HomeController : Controller
    {
        public IActionResult Main()
        {
            return View();
        }

        public IActionResult Advertisements()
        {
            return View("Main");
        }
        
        public IActionResult Profile()
        {
            return View("Main");
        }

        public IActionResult News()
        {
            return View("Main");
        }
        public IActionResult Articles()
        {
            return View("Main");
        }
    }
}