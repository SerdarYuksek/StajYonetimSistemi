using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StajYonetimGUI.Models.User;
using System.Text;

namespace StajYonetimGUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(HttpClient httpClient)
        {
            _httpClient = HttpClientFactory.Create();
            _httpClient.BaseAddress = new Uri("http://localhost:2000"); // Ocelot'un çalıştığı adres
        }

        public IActionResult SingInAsync()
        {
            return View();
        }

        public async Task<IActionResult> SingInAsync(LoginViewModel loginViewModel)
        {

            var jsonContent = new StringContent(JsonConvert.SerializeObject(loginViewModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Account/UserSignIn", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return View();
            }
            else
            {
                return View(response.RequestMessage);
            }
        }

        public async Task<IActionResult> LogOutAsync()
        {
            var response = await _httpClient.GetAsync("/Account/UserLogOut");

            if (response.IsSuccessStatusCode)
            {
                return View("SingInAsync");
            }
            else
            {
                return View(response.RequestMessage);
            }
        }

        public IActionResult SignUpAsync()
        {
            return View();
        }

        public async Task<IActionResult> SignUpAsync(AppUser User)
        {

            var jsonContent = new StringContent(JsonConvert.SerializeObject(User), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Account/UserSignUp", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return View("SingInAsync");
            }
            else
            {
                return View(response.RequestMessage);
            }
        }

    }
}

