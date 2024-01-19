using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StajYonetimGUI.Models.User;
using System.Text;

namespace StajYonetimGUI.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;

        public UserController(HttpClient httpClient)
        {
            _httpClient = HttpClientFactory.Create();
            _httpClient.BaseAddress = new Uri("http://localhost:2000"); // Ocelot'un çalıştığı adres
        }

        public async Task<IActionResult> UserListAsync(string roles, int imageid, int page = 1)
        {
            // Query string'i oluştur
            var userQueryString = $"?roles={roles}&page={page}";

            // User servisine ve Image servisine istekte bulun
            var userResponseTask = _httpClient.GetAsync("/User/UserList" + userQueryString);
            var imageResponseTask = _httpClient.GetAsync($"/Image/GetImage/{imageid}");

            // Her iki task'in tamamlanmasını bekle
            await Task.WhenAll(userResponseTask, imageResponseTask);

            // Her iki isteğin de başarılı olup olmadığını kontrol et
            if (userResponseTask.Result.IsSuccessStatusCode && imageResponseTask.Result.IsSuccessStatusCode)
            {
                var userData = await userResponseTask.Result.Content.ReadAsAsync<UserListResponseModel>();
                var imageData = await imageResponseTask.Result.Content.ReadAsAsync<ImageResponseModel>();

                return View(new { UserData = userData, ImageData = imageData });
            }
            else
            {
                // Herhangi bir istek başarısız olursa bu durumu ele al
                return View(userResponseTask.Result.RequestMessage);
            }
        }

        public async Task<IActionResult> UserDeleteAsync(int userId, int imageId)
        {
            var userResponseTask = await _httpClient.DeleteAsync($"/User/UserDelete/{userId}");
            var imageResponseTask = await _httpClient.DeleteAsync($"/Image/DeleteImage/{imageId}");

            if (userResponseTask.IsSuccessStatusCode && imageResponseTask.IsSuccessStatusCode)
            {
                return View("UserListAsync");
            }
            else
            {
                return View(userResponseTask.RequestMessage);
            }
        }

        public async Task<IActionResult> UserUpdateAsync(int userId, int imageId)
        {
            var userResponseTask = await _httpClient.GetAsync($"/User/UserList/{userId}");
            var imageResponseTask = await _httpClient.GetAsync($"/Image/GetImage/{imageId}");

            if (userResponseTask.IsSuccessStatusCode && imageResponseTask.IsSuccessStatusCode)
            {
                var userData = await userResponseTask.Content.ReadAsAsync<UserUpdateResponseModel>();
                var imageData = await imageResponseTask.Content.ReadAsAsync<ImageResponseModel>();
                return View(new { UserData = userData, ImageData = imageData });
            }
            else
            {
                return View(userResponseTask.RequestMessage);
            }
        }

        public async Task<IActionResult> UserUpdateAsync(AppUser appUser, IFormFile file)
        {
            var userJsonContent = new StringContent(JsonConvert.SerializeObject(appUser), Encoding.UTF8, "application/json");
            var userResponse = await _httpClient.PutAsync("/User/UserUpdate", userJsonContent);
            var imageJsonContent = new StringContent(JsonConvert.SerializeObject(file), Encoding.UTF8, "application/json");
            var imageResponse = await _httpClient.PutAsync("/Image/ImageUpdate", imageJsonContent);

            if (userResponse.IsSuccessStatusCode && imageResponse.IsSuccessStatusCode)
            {
                return View(); // Student veya Personel Dashboard ekranlarına gönder
            }
            else
            {
                return View(userResponse.RequestMessage);
            }
        }

        public async Task<IActionResult> RegistrationConfirmAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/User/RegistrationConfirm/{id}");

            if (response.IsSuccessStatusCode)
            {
                return View("UserListAsync");
            }
            else
            {
                return View(response.RequestMessage);
            }
        }

        public async Task<IActionResult> RegistrationDeclineAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/User/RegistrationDecline/{id}");

            if (response.IsSuccessStatusCode)
            {
                return View("UserListAsync");
            }
            else
            {
                return View(response.RequestMessage);
            }
        }
    }
}
