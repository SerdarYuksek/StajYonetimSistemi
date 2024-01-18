using ImageService.Api.CQRS.Commands.SaveImage;
using ImageService.Api.Models;
using ImageService.Api.Services;
using MediatR;

namespace ImageService.Api.CQRS.Handles
{
    public class SaveImageCommandHandler : IRequestHandler<SaveImageCommadRequest, SaveImageCommandResponse>
    {
        private readonly MongoDbService _mongoDbService;

        public SaveImageCommandHandler(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        public async Task<SaveImageCommandResponse> Handle(SaveImageCommadRequest request, CancellationToken cancellationToken)
        {
            // Fotoğraf Dosyasının boyutu kontrolü
            if (request.File.Length == 0)
            {
                throw new PhotoNotFoundException("Fotoğraf bulunamadı.");
            }

            // Fotoğraf Dosyasının adını ve uzantısını al
            var imageName = Path.GetFileNameWithoutExtension(request.File.FileName);
            var sanitizedImageFileName = string.Join("_", imageName.Split(Path.GetInvalidFileNameChars()));
            var imageFileExtension = Path.GetExtension(request.File.FileName);

            // Fotoğraf Dosyasının kaydedileceği klasörü belirle
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images");

            // Fotoğraf Dosyasının kaydet
            var imageFileNameWithGuid = $"{sanitizedImageFileName}_{Guid.NewGuid()}";
            var imageFileNameWithExtension = $"{imageFileNameWithGuid}{imageFileExtension}";
            var imageFilePath = Path.Combine(uploadFolder, imageFileNameWithExtension);

            using (var stream = new FileStream(imageFilePath, FileMode.Create))
            {
                request.File.CopyTo(stream);
            }

            // Fotoğraf dosyasının bilgilerini MongoDB'ye kaydet
            var newPhoto = new ImageInfo
            {
                ImageFileName = imageFileNameWithGuid,
                ImageFileType = imageFileExtension,
                UserNo = request.UserNo,
                ImageFileSize = request.File.Length,
                ImageUploadDate = DateTime.UtcNow,
            };

            //Veri tabanına bilgilerin kaydedilmesi
            _mongoDbService.SavePhotoInfo(newPhoto);
            return new();
        }
    }
}
