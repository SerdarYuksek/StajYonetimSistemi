using Microsoft.AspNetCore.Mvc;
using StajYonetimGUI.Models;
using System.Diagnostics;

namespace StajYonetimGUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

       
        public IActionResult Loginpage()
        {
            return View();
        }

        public IActionResult AdminPanel()
        {
            return View();
        }
        public IActionResult UserPage()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
