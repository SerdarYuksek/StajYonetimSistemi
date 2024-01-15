using ImageService.Api.CQRS.Queries.GetImage;
using ImageService.Api.Services;
using MediatR;

namespace ImageService.Api.CQRS.Handles
{
    public class GetImageQueryHandle : IRequestHandler<GetImageQueryRequest, GetImageQueryResponse>
    {
        private readonly MongoDbService _mongoDbService;

        public GetImageQueryHandle(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        public async Task<GetImageQueryResponse> Handle(GetImageQueryRequest request, CancellationToken cancellationToken)
        {
            var photo = _mongoDbService.GetPhoto(request.Id);

            if (photo == null)
            {
                throw new PhotoNotFoundException("Fotoğraf bulunamadı.");
            }

            var imageExtension = photo.ImageFileType;
            var imageFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", photo.ImageFileName + imageExtension);

            return new GetImageQueryResponse
            {
                Bytes = File.ReadAllBytes(imageFilePath),
                ImageName = photo.ImageFileName + imageExtension
            };
        }

    }
}
