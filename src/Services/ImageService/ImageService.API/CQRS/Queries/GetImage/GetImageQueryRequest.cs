using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ImageService.Api.CQRS.Queries.GetImage
{
    public class GetImageQueryRequest : IRequest<GetImageQueryResponse>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
