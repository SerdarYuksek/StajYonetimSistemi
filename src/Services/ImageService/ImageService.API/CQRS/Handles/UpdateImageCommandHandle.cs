using ImageService.Api.CQRS.Commands.UpdateImage;
using ImageService.Api.Models;
using ImageService.Api.Services;
using MediatR;

namespace ImageService.Api.CQRS.Handles
{
    public class UpdateImageCommandHandle : IRequestHandler<UpdateImageCommadRequest, UpdateImageCommandResponse>
    {
        private readonly MongoDbService _mongoDbService;

        public UpdateImageCommandHandle(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        public async Task<UpdateImageCommandResponse> Handle(UpdateImageCommadRequest request, CancellationToken cancellationToken)
        {
            var photo = _mongoDbService.GetPhoto(request.Id);
            if (photo == null)
            {
                throw new PhotoNotFoundException("Fotoğraf bulunamadı.");
            }

            if (photo.UserNo != null)
            {
                // Öğrenci Numarasına göre ilgili Resim bulunur ve bilgileri atanır
                var existingPhoto = _mongoDbService.GetAllPhotos().FirstOrDefault(p => p.UserNo == photo.UserNo);

                // Eğer kullanıcıya ait bir fotoğraf varsa, silinir

                // Dosyayı sil
                var existingfilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", existingPhoto.ImageFileName + existingPhoto.ImageFileType);
                File.Delete(existingfilePath);

                // MongoDB'den de sil
                _mongoDbService.DeletePhoto(existingPhoto.Id);

            }

            // Yeni Resimi kaydet

            // Fotoğraf Dosyasının adını ve uzantısını al
            var imageName = Path.GetFileNameWithoutExtension(request.file.FileName);
            var sanitizedImageFileName = string.Join("_", imageName.Split(Path.GetInvalidFileNameChars()));
            var imageFileExtension = Path.GetExtension(request.file.FileName);

            // Fotoğraf Dosyasının kaydedileceği klasörü belirle
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images");

            // Fotoğraf Dosyasının kaydet
            var imageFileNameWithGuid = $"{sanitizedImageFileName}_{Guid.NewGuid()}";
            var imageFileNameWithExtension = $"{imageFileNameWithGuid}{imageFileExtension}";
            var imageFilePath = Path.Combine(uploadFolder, imageFileNameWithExtension);

            using (var stream = new FileStream(imageFilePath, FileMode.Create))
            {
                request.file.CopyTo(stream);
            }

            // Fotoğraf dosyasının bilgilerini MongoDB'ye kaydet
            var newPhoto = new ImageInfo
            {
                ImageFileName = imageFileNameWithGuid,
                ImageFileType = imageFileExtension,
                UserNo = photo.UserNo,
                ImageFileSize = request.file.Length,
                ImageUploadDate = DateTime.UtcNow,
            };

            _mongoDbService.SavePhotoInfo(newPhoto);
            return new();
        }
    }
}
