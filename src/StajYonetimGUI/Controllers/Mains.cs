using Microsoft.AspNetCore.Mvc;

namespace StajYonetimGUI.Controllers
{
    public class Mains : Controller
    {
        public IActionResult PerosnalMain()
        {
            return View();
        }
        public IActionResult StudentMain()
        {
            return View();
        }
    }
}
