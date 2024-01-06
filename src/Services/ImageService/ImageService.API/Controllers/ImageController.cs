using ImageService.Api.Models;
using ImageService.Api.Services;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult SaveImage(string userNo, IFormFile file)
        {
            try
            {
                // Fotoğraf Dosyasının boyutu kontrolü
                if (file.Length == 0)
                {
                    return BadRequest("Dosya boş.");
                }

                // Fotoğraf Dosyasının adını ve uzantısını al
                var imageName = Path.GetFileNameWithoutExtension(file.FileName);
                var sanitizedImageFileName = string.Join("_", imageName.Split(Path.GetInvalidFileNameChars()));
                var imageFileExtension = Path.GetExtension(file.FileName);

                // Fotoğraf Dosyasının kaydedileceği klasörü belirle
                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images");

                // Fotoğraf Dosyasının kaydet
                var imageFileNameWithGuid = $"{sanitizedImageFileName}_{Guid.NewGuid()}";
                var imageFileNameWithExtension = $"{imageFileNameWithGuid}{imageFileExtension}";
                var imageFilePath = Path.Combine(uploadFolder, imageFileNameWithExtension);

                using (var stream = new FileStream(imageFilePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Fotoğraf dosyasının bilgilerini MongoDB'ye kaydet
                var newPhoto = new ImageInfo
                {
                    ImageFileName = imageFileNameWithGuid,
                    ImageFileType = imageFileExtension,
                    UserNo = userNo,
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

                // Dosya türünü belirle
                var imageExtension = photo.ImageFileType;

                // Dosyanın bulunduğu klasörü belirle
                var imageFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", photo.ImageFileName + imageExtension);

                // Resim bytelarını UI tarafına gönder
                return Ok(new ImageResponseModel { Bytes = System.IO.File.ReadAllBytes(imageFilePath), ImageName = photo.ImageFileName + imageExtension });
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
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", photo.ImageFileName + photo.ImageFileType);
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
        [HttpPost("UpdatePhoto{id}")]
        public IActionResult UpdatePhoto(string id, IFormFile file)
        {
            try
            {

                var photo = _mongoDbService.GetPhoto(id);
                if (photo == null)
                {
                    return NotFound("Fotoğraf bulunamadı.");
                }

                if (photo.UserNo != null)
                {
                    // Öğrenci Numarasına göre ilgili Resim bulunur ve bilgileri atanır
                    var existingPhoto = _mongoDbService.GetAllPhotos().FirstOrDefault(p => p.UserNo == photo.UserNo);

                    // Eğer kullanıcıya ait bir fotoğraf varsa, silinir

                    // Dosyayı sil
                    var existingfilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", existingPhoto.ImageFileName + existingPhoto.ImageFileType);
                    System.IO.File.Delete(existingfilePath);

                    // MongoDB'den de sil
                    _mongoDbService.DeletePhoto(existingPhoto.Id);

                }

                // Yeni Resimi kaydet

                // Fotoğraf Dosyasının adını ve uzantısını al
                var imageName = Path.GetFileNameWithoutExtension(file.FileName);
                var sanitizedImageFileName = string.Join("_", imageName.Split(Path.GetInvalidFileNameChars()));
                var imageFileExtension = Path.GetExtension(file.FileName);

                // Fotoğraf Dosyasının kaydedileceği klasörü belirle
                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images");

                // Fotoğraf Dosyasının kaydet
                var imageFileNameWithGuid = $"{sanitizedImageFileName}_{Guid.NewGuid()}";
                var imageFileNameWithExtension = $"{imageFileNameWithGuid}{imageFileExtension}";
                var imageFilePath = Path.Combine(uploadFolder, imageFileNameWithExtension);

                using (var stream = new FileStream(imageFilePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Fotoğraf dosyasının bilgilerini MongoDB'ye kaydet
                var newPhoto = new ImageInfo
                {
                    ImageFileName = imageFileNameWithGuid,
                    ImageFileType = imageFileExtension,
                    UserNo = photo.UserNo,
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
