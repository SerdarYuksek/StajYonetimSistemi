using FileService.Api.Models;
using FileService.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        //MongoDB Servicesinden türetilmiş nesne
        private readonly MongoDbService _mongoDbService;

        //Üretilen nesnenin FileControllerda kullanılması için contructırda tanımlanması
        public FileController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        //Dosya Yükleme İşlemi 
        [HttpPost("UploadFile")]
        public IActionResult UploadFile(string? userNo, int? fileTypeNumber, int? internNumber, int? fileNumber, IFormFile file)
        {
            try
            {
                // Dosya boyutu kontrolü
                if (file.Length == 0)
                {
                    return BadRequest("Dosya boş.");
                }

                // Dosya adını ve uzantısını al
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var sanitizedFileName = string.Join("_", fileName.Split(Path.GetInvalidFileNameChars()));
                var fileExtension = Path.GetExtension(file.FileName);

                // PDF dosyası kontrolü
                if (fileExtension.ToLower() != ".pdf")
                {
                    return BadRequest("Sadece PDF dosyaları kabul edilmektedir.");
                }

                // Dosyanın kaydedileceği klasörü belirle
                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files");

                // Dosyayı kaydet
                var fileNameWithGuid = $"{sanitizedFileName}_{Guid.NewGuid()}";
                var fileNameWithExtension = $"{fileNameWithGuid}{fileExtension}";
                var filePath = Path.Combine(uploadFolder, fileNameWithExtension);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Dosya bilgilerini MongoDB'ye kaydet
                var newFile = new FileDataInfo
                {
                    FileName = fileNameWithGuid,
                    FileType = fileExtension,
                    UserNo = userNo,
                    FileTypeNumber = fileTypeNumber,
                    FileNumber = fileNumber,
                    InternNumber = internNumber,
                    FileSize = file.Length,
                    UploadDate = DateTime.UtcNow,
                };

                _mongoDbService.SaveFileInfo(newFile);

                return Ok("Dosya başarıyla kaydedildi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //Dosya Silme İşlemi
        [HttpDelete("DeleteFile/{id}")]
        public IActionResult DeleteFile(string id)
        {
            try
            {
                //Dosya bilgilerinin boş durumunun kontrolü
                var file = _mongoDbService.GetFile(id);
                if (file == null)
                {
                    return NotFound("Dosya bulunamadı.");
                }

                // Dosya uzantısını al
                var fileExtension = Path.GetExtension(file.FileName);

                // Dosyayı sil
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", file.FileName + fileExtension);
                System.IO.File.Delete(filePath);

                // MongoDB'den de sil
                _mongoDbService.DeleteFile(id);

                return Ok("Dosya başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //Dosya bilgisi Getirme ve Listeleme İşlemi
        [HttpGet("GetFiles/{id}")]
        public IActionResult GetFiles(string id)
        {
            try
            {
                //Dosya bilgilerinin boş durumunun kontrolü
                var fileInfo = _mongoDbService.GetFile(id);
                if (fileInfo == null)
                {
                    return NotFound("Dosya bulunamadı.");
                }

                if (fileInfo.FileTypeNumber != 0)
                {
                    // Dosya türüne göre dosyaları getir
                    var filesByType = _mongoDbService.GetFilesByType(fileInfo.FileTypeNumber);
                    return Ok(filesByType);
                }
                else if (fileInfo.UserNo != null && fileInfo.InternNumber != 0 && fileInfo.FileNumber != 0)
                {
                    // Kullanıcı numarası, stajyer numarası ve dosya numarasına göre belirli bir dosyayı getir
                    var file = _mongoDbService.GetFileByUserInternAndNumber(fileInfo.UserNo, fileInfo.InternNumber, fileInfo.FileNumber);
                    return Ok(file);
                }
                else
                {
                    return BadRequest("Getirilecek/Listelenecek Dosya Bilgilerine erişilemiyor.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //Dosya Güncelleme İşlemi
        [HttpPost("UpdateFile/{id}")]
        public IActionResult UpdateFile(string id, IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("Dosya boş.");
                }

                //Dosya bilgilerinin boş durumunun kontrolü
                var fileInfo = _mongoDbService.GetFile(id);
                if (fileInfo == null)
                {
                    return NotFound("Dosya bulunamadı.");
                }

                //Dosya çeşidi Numarası Boş değilse Staj Dökümanlarını Güncelleme işlemini yapar
                if (fileInfo.FileTypeNumber != null)
                {
                    // Dosya Türü Numarası göre ilgili dosya bulunur ve bilgileri atanır
                    var existingFile = _mongoDbService.GetAllFiles().FirstOrDefault(p => p.FileTypeNumber == fileInfo.FileTypeNumber);

                    // Eğer kullanıcıya ait bir fotoğraf varsa, önce silelim

                    // Dosyayı sil
                    var existingfilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", existingFile.FileName + existingFile.FileType);
                    System.IO.File.Delete(existingfilePath);

                    // MongoDB'den de sil
                    _mongoDbService.DeleteFile(existingFile.Id);

                    // Yeni dosyayı kaydet

                    // Dosya adını ve uzantısını al
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var sanitizedFileName = string.Join("_", fileName.Split(Path.GetInvalidFileNameChars()));
                    var fileExtension = Path.GetExtension(file.FileName);

                    // PDF dosyası kontrolü
                    if (fileExtension.ToLower() != ".pdf")
                    {
                        return BadRequest("Sadece PDF dosyaları kabul edilmektedir.");
                    }

                    // Dosyanın kaydedileceği klasörü belirle
                    var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files");

                    // Dosyayı kaydet
                    var fileNameWithGuid = $"{sanitizedFileName}_{Guid.NewGuid()}";
                    var fileNameWithExtension = $"{fileNameWithGuid}{fileExtension}";
                    var filePath = Path.Combine(uploadFolder, fileNameWithExtension);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    // Dosya bilgilerini MongoDB'ye kaydet
                    var newFile = new FileDataInfo
                    {
                        FileName = fileNameWithGuid,
                        FileType = fileExtension,
                        FileTypeNumber = fileInfo.FileTypeNumber,
                        FileSize = file.Length,
                        UploadDate = DateTime.UtcNow,
                    };

                    _mongoDbService.SaveFileInfo(newFile);

                    return Ok("Dosya başarıyla güncellendi ve kaydedildi.");
                }

                //Öğrenci Numarası , Staj Numarası ve dosya numarası Boş değilse Staj Dökümanlarını Güncelleme işlemini yapar
                else if (fileInfo.UserNo != null && fileInfo.InternNumber != null && fileInfo.FileNumber != null)
                {
                    //Öğrenci Numarası , Staj Numarası ve dosya numarasına göre ilgili dosya bulunur ve bilgileri atanır
                    var existingFile = _mongoDbService.GetAllFiles().FirstOrDefault(p => p.UserNo == fileInfo.UserNo && p.InternNumber == fileInfo.InternNumber && p.FileNumber == fileInfo.InternNumber);

                    // Eğer kullanıcıya ait bir dosya varsa, önce silelim

                    // Dosyayı sil
                    var existingfilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", existingFile.FileName + existingFile.FileType);
                    System.IO.File.Delete(existingfilePath);

                    // MongoDB'den de sil
                    _mongoDbService.DeleteFile(existingFile.Id);

                    // Yeni Dosyayı kaydet

                    // Dosya adını ve uzantısını al
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var sanitizedFileName = string.Join("_", fileName.Split(Path.GetInvalidFileNameChars()));
                    var fileExtension = Path.GetExtension(file.FileName);

                    // PDF dosyası kontrolü
                    if (fileExtension.ToLower() != ".pdf")
                    {
                        return BadRequest("Sadece PDF dosyaları kabul edilmektedir.");
                    }

                    // Dosyanın kaydedileceği klasörü belirle
                    var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files");

                    // Dosyayı kaydet
                    var fileNameWithGuid = $"{sanitizedFileName}_{Guid.NewGuid()}";
                    var fileNameWithExtension = $"{fileNameWithGuid}{fileExtension}";
                    var filePath = Path.Combine(uploadFolder, fileNameWithExtension);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    // Dosya bilgilerini MongoDB'ye kaydet
                    var newFile = new FileDataInfo
                    {
                        FileName = fileNameWithGuid,
                        FileType = fileExtension,
                        UserNo = fileInfo.UserNo,
                        FileNumber = fileInfo.FileNumber,
                        InternNumber = fileInfo.InternNumber,
                        FileSize = file.Length,
                        UploadDate = DateTime.UtcNow,
                    };

                    _mongoDbService.SaveFileInfo(newFile);

                    return Ok("Dosya başarıyla güncellendi ve kaydedildi.");
                }
                else
                {
                    return BadRequest("Güncellenecek Dosya Bilgilerine erişilemiyor.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //Dosyayı Pdf Olarak İndirme İşlemi
        [HttpGet("DownloadPdfFile/{id}")]
        public IActionResult DownloadPdfFile(string id)
        {
            try
            {
                var file = _mongoDbService.GetFile(id);
                if (file == null)
                {
                    return NotFound("Dosya bulunamadı.");
                }

                // Dosya türünü belirle
                var fileExtension = file.FileType;

                // Dosya yolunu belirle
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", file.FileName + fileExtension);

                // Sadece PDF dosyalarını kontrol et
                if (fileExtension.ToLower() != ".pdf")
                {
                    return BadRequest("Sadece PDF dosyaları kabul edilmektedir.");
                }

                // Dosya bytelarını UI tarafına gönder
                return Ok(new FileResponseModel { Bytes = System.IO.File.ReadAllBytes(filePath), FileName = file.FileName + fileExtension });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //Dosyayı Pdf olarak Web Tarayıcısında Görüntüleme İşlemi
        [HttpGet("ShowPdfFile/{id}")]
        public IActionResult ShowPdfFile(string id)
        {
            var file = _mongoDbService.GetFile(id);

            var fileExtension = file.FileType;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", file.FileName + fileExtension);

            // Sadece PDF dosyalarını kontrol et
            if (fileExtension.ToLower() != ".pdf")
            {
                return BadRequest("Sadece PDF dosyaları kabul edilmektedir.");
            }

            // Dosya bytelarını UI tarafına gönder
            return Ok(new FileResponseModel { Bytes = System.IO.File.ReadAllBytes(filePath), FileName = file.FileName + fileExtension });

        }
    }
}

