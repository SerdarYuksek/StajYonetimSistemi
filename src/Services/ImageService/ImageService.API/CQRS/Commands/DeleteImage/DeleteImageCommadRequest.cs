using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ImageService.Api.CQRS.Commands.DeleteImage
{
    public class DeleteImageCommadRequest : IRequest<DeleteImageCommandResponse>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
