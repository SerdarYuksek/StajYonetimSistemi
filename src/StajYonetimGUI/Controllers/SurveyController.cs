using Microsoft.AspNetCore.Mvc;

namespace StajYonetimGUI.Controllers
{
    public class SurveyController : Controller
    {
        private readonly HttpClient _httpClient;

        public SurveyController(HttpClient httpClient)
        {
            _httpClient = HttpClientFactory.Create();
            _httpClient.BaseAddress = new Uri("http://localhost:2000"); // Ocelot'un çalıştığı adres
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
