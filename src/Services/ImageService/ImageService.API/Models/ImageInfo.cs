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
        public string ImageFileName { get; set; }

        [BsonElement("UserNo")]
        public string UserNo { get; set; }

        [BsonElement("FileType")]
        public string ImageFileType { get; set; }

        [BsonElement("FileSize")]
        public long ImageFileSize { get; set; }

        [BsonElement("UploadDate")]
        public DateTime ImageUploadDate { get; set; }
    }
}
