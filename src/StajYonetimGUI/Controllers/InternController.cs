using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StajYonetimGUI.Models.Intern;
using System.Text;

namespace StajYonetimGUI.Controllers
{
    public class InternController : Controller
    {
        private readonly HttpClient _httpClient;

        public InternController(HttpClient httpClient)
        {
            _httpClient = HttpClientFactory.Create();
            _httpClient.BaseAddress = new Uri("http://localhost:2000"); // Ocelot'un çalıştığı adres
        }

        public IActionResult CreateInternAsync()
        {
            return View();
        }

        public async Task<IActionResult> CreateInternAsync(InternInfo ınternInfo)
        {
            var internJsonContent = new StringContent(JsonConvert.SerializeObject(ınternInfo), Encoding.UTF8, "application/json");
            var internResponse = await _httpClient.PostAsync("/Intern/CreateIntern", internJsonContent);

            if (internResponse.IsSuccessStatusCode)
            {
                return View(); //Öğrenci home
            }
            else
            {
                return View(internResponse.RequestMessage);
            }
        }

        public async Task<IActionResult> ListInternAsync(int id)
        {
            var internResponse = await _httpClient.GetAsync($"/Intern/ListIntern{id}");

            // Her iki isteğin de başarılı olup olmadığını kontrol et
            if (internResponse.IsSuccessStatusCode)
            {
                var internData = await internResponse.Content.ReadAsAsync<InternListResponseModel>();

                return View(internData);
            }
            else
            {
                // Herhangi bir istek başarısız olursa bu durumu ele al
                return View(internResponse.RequestMessage);
            }
        }

        public async Task<IActionResult> ListInternDetailAsync(int id)
        {
            var internResponse = await _httpClient.GetAsync($"/Intern/ListInternDetail{id}");

            if (internResponse.IsSuccessStatusCode)
            {
                var internData = await internResponse.Content.ReadAsAsync<InternInfo>();
                return View(internData);
            }
            else
            {
                // Herhangi bir istek başarısız olursa bu durumu ele al
                return View(internResponse.RequestMessage);
            }
        }

        public async Task<IActionResult> UserDeleteAsync(int id)
        {
            var userResponse = await _httpClient.DeleteAsync($"/Intern/InternDelete/{id}");

            if (userResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("ListInternAsync", id);
            }
            else
            {
                return View(userResponse.RequestMessage);
            }
        }

        public async Task<IActionResult> InternConfirmAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/Intern/InternConfirm/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListInternAsync", id);
            }
            else
            {
                return View(response.RequestMessage);
            }
        }

        public async Task<IActionResult> ConfirmDeclineAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/Intern/ConfirmDecline/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListInternAsync", id);
            }
            else
            {
                return View(response.RequestMessage);
            }
        }

        public async Task<IActionResult> InternAcceptAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/Intern/InternAccept/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListInternAsync", id);
            }
            else
            {
                return View(response.RequestMessage);
            }
        }

        public async Task<IActionResult> InternAcceptDeclineAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/Intern/InternAcceptDecline/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListInternAsync", id);
            }
            else
            {
                return View(response.RequestMessage);
            }
        }

        public async Task<IActionResult> InternRejectAsync(int id, string rejectReason)
        {
            var internQueryString = $"?id={id}&rejectReason={rejectReason}";
            var response = await _httpClient.GetAsync("/Intern/InternReject" + internQueryString);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListInternAsync", id);
            }
            else
            {
                return View(response.RequestMessage);
            }
        }

        public async Task<IActionResult> IContributConfirmAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/Intern/ContributConfirm/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListInternAsync", id);
            }
            else
            {
                return View(response.RequestMessage);
            }
        }

        public async Task<IActionResult> ContributDeclineAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/Intern/ContributDecline/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListInternAsync", id);
            }
            else
            {
                return View(response.RequestMessage);
            }
        }
    }
}
