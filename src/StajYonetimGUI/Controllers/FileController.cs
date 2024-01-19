using Microsoft.AspNetCore.Mvc;
using StajYonetimGUI.Models.File;

namespace StajYonetimGUI.Controllers
{
    public class FileController : Controller
    {
        private readonly HttpClient _httpClient;

        public FileController(HttpClient httpClient)
        {
            _httpClient = HttpClientFactory.Create();
            _httpClient.BaseAddress = new Uri("http://localhost:2000"); // Ocelot'un çalıştığı adres
        }

        public async Task<IActionResult> UploadFileAsync(string userNo, int fileTypeNumber, int internNumber, int fileNumber, IFormFile file)
        {
            try
            {
                // Dosyayı API'ye yükleme işlemi için gerekli parametreleri oluştur
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(userNo), "userNo");
                content.Add(new StringContent(fileTypeNumber.ToString()), "fileTypeNumber");
                content.Add(new StringContent(internNumber.ToString()), "internNumber");
                content.Add(new StringContent(fileNumber.ToString()), "fileNumber");
                content.Add(new StreamContent(file.OpenReadStream()), "file", file.FileName);

                // API'ye isteği gönder
                var response = await _httpClient.PostAsync("/File/UploadFile", content);

                // API'den dönen cevabı kontrol et
                if (response.IsSuccessStatusCode)
                {
                    return View(); // main
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return BadRequest(errorMessage);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        public async Task<IActionResult> DeleteFileAsync(int id)
        {
            var fileResponse = await _httpClient.DeleteAsync($"/File/DeleteFile/{id}");

            if (fileResponse.IsSuccessStatusCode)
            {

                return View(); //main
            }
            else
            {
                return View(fileResponse.RequestMessage);
            }
        }

        public async Task<IActionResult> GetFilesAsync(int id)
        {
            var fileResponse = await _httpClient.DeleteAsync($"/File/GetFiles/{id}");

            if (fileResponse.IsSuccessStatusCode)
            {
                var fileData = fileResponse.Content.ReadAsAsync<FileResponseModel>().Result;

                // Dosyayı tarayıcıya indirme işlemini başlat
                return File(fileData.Bytes, "application/pdf", fileData.FileName);
            }
            else
            {
                var errorMessage = fileResponse.Content.ReadAsStringAsync().Result;
                ViewBag.ErrorMessage = errorMessage;
                return View("ErrorView");
            }
        }

        public async Task<IActionResult> UpdateFileAsync(string id, IFormFile file)
        {
            var userQueryString = $"?id={id}&file={file}";
            var fileResponse = await _httpClient.GetAsync("/File/UpdateFile" + userQueryString);

            // API'den dönen cevabı kontrol et
            if (fileResponse.IsSuccessStatusCode)
            {
                return View(); // main
            }
            else
            {
                var errorMessage = await fileResponse.Content.ReadAsStringAsync();
                return BadRequest(errorMessage);
            }
        }

        public async Task<IActionResult> DownloadPdfFileAsync(string id)
        {
            var fileResponse = await _httpClient.GetAsync($"/File/DownloadPdfFile/{id}");

            if (fileResponse.IsSuccessStatusCode)
            {
                var fileData = fileResponse.Content.ReadAsAsync<FileResponseModel>().Result;

                // Dosyayı tarayıcıya indirme işlemini başlat
                return File(fileData.Bytes, "application/pdf", fileData.FileName);
            }
            else
            {
                var errorMessage = fileResponse.Content.ReadAsStringAsync().Result;
                ViewBag.ErrorMessage = errorMessage;
                return View("ErrorView");
            }
        }

        public async Task<IActionResult> ShowPdfFileAsync(string id)
        {
            var fileResponse = await _httpClient.GetAsync($"/File/ShowPdfFile/{id}");

            if (fileResponse.IsSuccessStatusCode)
            {
                var fileData = fileResponse.Content.ReadAsAsync<FileResponseModel>().Result;

                // Dosyayı tarayıcıya indirme işlemini başlat
                return File(fileData.Bytes, "application/pdf", fileData.FileName);
            }
            else
            {
                var errorMessage = fileResponse.Content.ReadAsStringAsync().Result;
                ViewBag.ErrorMessage = errorMessage;
                return View("ErrorView");
            }
        }
    }
}
