using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ImageService.Api.CQRS.Commands.UpdateImage
{
    public class UpdateImageCommadRequest : IRequest<UpdateImageCommandResponse>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public IFormFile file { get; set; }
    }
}
