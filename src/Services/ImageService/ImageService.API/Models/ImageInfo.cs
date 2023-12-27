using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ImageService.Api.Models
{
    //Bir resimle alakalı bilgilerin veritabanında tutulması için gerekli entityler (BSON Formatında)
    public class ImageInfo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("FileName")]
        public string FileName { get; set; }

        [BsonElement("UserNo")]
        public string UserNo { get; set; }

        [BsonElement("FileType")]
        public string FileType { get; set; }

        [BsonElement("FileSize")]
        public long FileSize { get; set; }

        [BsonElement("UploadDate")]
        public DateTime UploadDate { get; set; }
    }
}
