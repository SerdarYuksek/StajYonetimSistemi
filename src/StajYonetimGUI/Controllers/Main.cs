using Microsoft.AspNetCore.Mvc;

namespace StajYonetimGUI.Controllers
{
    public class Main : Controller
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
