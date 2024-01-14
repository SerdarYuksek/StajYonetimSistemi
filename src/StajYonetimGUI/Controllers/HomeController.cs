using Microsoft.AspNetCore.Mvc;
using StajYonetimGUI.Models;
using System.Diagnostics;

namespace StajYonetimGUI.Controllers
{
    public class HomeController : Controller
    {
        

       

       
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
        public IActionResult InternPage()
        {
            return View();
        }
        public IActionResult Anket()
        {
            return View();
        }
        public IActionResult File()
        {
            return View();
        }
        public IActionResult image()
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
