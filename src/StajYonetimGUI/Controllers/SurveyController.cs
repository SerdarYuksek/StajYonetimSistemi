using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StajYonetimGUI.Models.Survey;
using System.Text;

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

        public IActionResult AddQuestionAsync()
        {
            return View();
        }

        public async Task<IActionResult> AddQuestionAsync(SurveyQuestion surveyQuestion)
        {
            var surveyJsonContent = new StringContent(JsonConvert.SerializeObject(surveyQuestion), Encoding.UTF8, "application/json");
            var surveyResponse = await _httpClient.PostAsync("/Survey/AddQuestion", surveyJsonContent);

            if (surveyResponse.IsSuccessStatusCode)
            {
                var surveyData = await surveyResponse.Content.ReadAsAsync<SurveyQuestion>();
                return RedirectToAction("SurveyListAsync", surveyData.ID);
            }
            else
            {
                return View(surveyResponse.RequestMessage);
            }
        }

        public async Task<IActionResult> QuestionDeleteAsync(int id)
        {
            var surveyResponse = await _httpClient.DeleteAsync($"/Survey/QuestionDelete/{id}");

            if (surveyResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("SurveyListAsync", id);
            }
            else
            {
                return View(surveyResponse.RequestMessage);
            }
        }

        public async Task<IActionResult> QuestionUpdateAsync(int id)
        {
            var surveyResponse = await _httpClient.GetAsync($"/Survey/QuestionUpdate{id}");

            // Her iki isteğin de başarılı olup olmadığını kontrol et
            if (surveyResponse.IsSuccessStatusCode)
            {
                var internData = await surveyResponse.Content.ReadAsAsync<SurveyQuestion>();

                return View(internData);
            }
            else
            {
                // Herhangi bir istek başarısız olursa bu durumu ele al
                return View(surveyResponse.RequestMessage);
            }
        }

        public async Task<IActionResult> QuestionUpdateAsync(SurveyQuestion surveyQuestion)
        {
            var surveyJsonContent = new StringContent(JsonConvert.SerializeObject(surveyQuestion), Encoding.UTF8, "application/json");
            var surveyResponse = await _httpClient.PutAsync("/Survey/QuestionUpdate", surveyJsonContent);

            // Her iki isteğin de başarılı olup olmadığını kontrol et
            if (surveyResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("SurveyListAsync");
            }
            else
            {
                // Herhangi bir istek başarısız olursa bu durumu ele al
                return View(surveyResponse.RequestMessage);
            }
        }

        public async Task<IActionResult> ListSurveyAsync(int page)
        {

            var surveyQueryString = $"&page={page}";
            var surveyResponse = await _httpClient.GetAsync("/Survey/ListSurvey" + surveyQueryString);

            // Her iki isteğin de başarılı olup olmadığını kontrol et
            if (surveyResponse.IsSuccessStatusCode)
            {
                var surveyData = await surveyResponse.Content.ReadAsAsync<SurveyResponseModel>();
                return View(surveyData.CurrentSurvey);
            }
            else
            {
                // Herhangi bir istek başarısız olursa bu durumu ele al
                return View(surveyResponse.RequestMessage);
            }
        }

        public async Task<IActionResult> AnswerSaveAsync(SurveyAnswer surveyAnswer)
        {

            var surveyJsonContent = new StringContent(JsonConvert.SerializeObject(surveyAnswer), Encoding.UTF8, "application/json");
            var surveyResponse = await _httpClient.PostAsync("/Survey/SurveyAnswer", surveyJsonContent);

            // Her iki isteğin de başarılı olup olmadığını kontrol et
            if (surveyResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("ListSurveyAsync");
            }
            else
            {
                // Herhangi bir istek başarısız olursa bu durumu ele al
                return View(surveyResponse.RequestMessage);
            }
        }
    }
}
