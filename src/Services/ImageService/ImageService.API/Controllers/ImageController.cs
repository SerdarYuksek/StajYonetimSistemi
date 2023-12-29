using ImageService.Api.Models;
using ImageService.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace ImageService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        //MongoDB Servicesinden türetilmiş nesne
        private readonly MongoDbService _mongoDbService;

        //Üretilen nesnenin ImageControllerda kullanılması için contructırda tanımlanması
        public ImageController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        //Fotoğraf Kaydetme İşlemi
        [HttpPost("SaveImage")]
        public IActionResult SaveImage(string userNo,IFormFile file)
        {
            try
            {
                // Dosya boyutu kontrolü
                if (file.Length == 0)
                {
                    return BadRequest("Dosya boş.");
                }

                // Dosya adını ve uzantısını al
                var fileName = file.FileName;
                var fileExtension = Path.GetExtension(fileName);

                // Dosyayı kaydedilecek klasörü belirle
                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images");

                // Dosyayı kaydet
                var filePath = Path.Combine(uploadFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                // Fotoğraf bilgilerini MongoDB'ye kaydet
                var newPhoto = new  ImageInfo
                {
                    ImageFileName = fileName,
                    ImageFileType = fileExtension,
                    UserNo   = userNo,
                    ImageFileSize = file.Length,
                    ImageUploadDate = DateTime.UtcNow,
                };

                _mongoDbService.SavePhotoInfo(newPhoto);

                return Ok("Fotoğraf başarıyla kaydedildi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //Fotoğraf Görüntüleme İşlemi
        [HttpGet("GetImage/{id}")]
        public IActionResult GetImage(string id)
        {
            try
            {
                // Id değerine göre resim bilgilerini getirme
                var photo = _mongoDbService.GetPhoto(id);
                if (photo == null)
                {
                    return NotFound("Fotoğraf bulunamadı.");
                }

                // Dosyanın bulunduğu klasörü belirle
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", photo.ImageFileName);

                // Dosya türünü belirle
                var contentType = _mongoDbService.GetContentType(photo.ImageFileName);

                return File(System.IO.File.ReadAllBytes(filePath), contentType, photo.ImageFileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //Fotoğraf Silme İşlemi
        [HttpDelete("DeleteImage/{id}")]
        public IActionResult DeleteImage(string id)
        {
            try
            {
                var photo = _mongoDbService.GetPhoto(id);
                if (photo == null)
                {
                    return NotFound("Fotoğraf bulunamadı.");
                }

                // Dosyayı sil
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", photo.ImageFileName);
                System.IO.File.Delete(filePath);

                // MongoDB'den de sil
                _mongoDbService.DeletePhoto(id);

                return Ok("Fotoğraf başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //Fotoğraf Güncelleme İşlemi
        [HttpPost("UpdatePhoto")]
        public IActionResult UpdatePhoto(string userNo, IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("Dosya boş.");
                }

                // Kullanıcı numarasına göre ilgili fotoğrafı bul
                var existingPhoto = _mongoDbService.GetAllPhotos()
                    .FirstOrDefault(p => p.UserNo == userNo);

                // Eğer kullanıcıya ait bir fotoğraf varsa, önce silelim

                // Dosyayı sil
                var existingfilePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", existingPhoto.ImageFileName);
                System.IO.File.Delete(existingfilePath);

                // MongoDB'den de sil
                _mongoDbService.DeletePhoto(existingPhoto.Id);

                // Yeni fotoğrafı kaydet
                var fileName = file.FileName;
                var fileExtension = Path.GetExtension(fileName);

                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images");
                var filePath = Path.Combine(uploadFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                // Fotoğraf bilgilerini MongoDB'ye kaydet
                var newPhoto = new ImageInfo
                {
                    ImageFileName = fileName,
                    ImageFileType = fileExtension,
                    UserNo   = userNo,
                    ImageFileSize = file.Length,
                    ImageUploadDate = DateTime.UtcNow,
                };

                _mongoDbService.SavePhotoInfo(newPhoto);

                return Ok("Fotoğraf başarıyla güncellendi ve kaydedildi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
