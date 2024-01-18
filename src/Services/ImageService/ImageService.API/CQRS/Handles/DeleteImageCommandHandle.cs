using ImageService.Api.CQRS.Commands.DeleteImage;
using ImageService.Api.Services;
using MediatR;

namespace ImageService.Api.CQRS.Handles
{
    public class DeleteImageCommandHandle : IRequestHandler<DeleteImageCommadRequest, DeleteImageCommandResponse>
    {
        private readonly MongoDbService _mongoDbService;

        public DeleteImageCommandHandle(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        public async Task<DeleteImageCommandResponse> Handle(DeleteImageCommadRequest request, CancellationToken cancellationToken)
        {
            var photo = _mongoDbService.GetPhoto(request.Id);
            if (photo == null)
            {
                throw new PhotoNotFoundException("Fotoğraf bulunamadı.");
            }

            // Dosyayı sil
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", photo.ImageFileName + photo.ImageFileType);
            File.Delete(filePath);

            // MongoDB'den de sil
            _mongoDbService.DeletePhoto(request.Id);
            return new();

        }
    }
}
