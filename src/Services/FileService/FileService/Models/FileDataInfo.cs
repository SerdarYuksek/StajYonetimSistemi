using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FileService.Api.Models
{
    //Bir Dosyayla alakalı bilgilerin veritabanında tutulması için gerekli entityler (BSON Formatında)
    public class FileDataInfo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("FileName")]
        public string FileName { get; set; }

        // ----File No Bilgileri----
        // 1= Staj Akış Şeması
        // 2= Staj Değerlendirme Dökümanı
        // 3= Staj Yönerge Dökümanı
        // 4= Staj Başvuru Formu
        // 5= Staj Devam Formu
        // 6= Staj Değerlendirme Formu
        // 7= Staj Rapor Şablonu
       
        [BsonElement("FileTypeNumber")]
        public int? FileTypeNumber { get; set; }

        [BsonElement("InternNumber")]
        public int? InternNumber { get; set; }

        [BsonElement("FileNumber")]
        public int? FileNumber { get; set; }

        [BsonElement("UserNo")]
        public string? UserNo { get; set; }

        [BsonElement("FileType")]
        public string FileType { get; set; }

        [BsonElement("FileSize")]
        public long FileSize { get; set; }

        [BsonElement("UploadDate")]
        public DateTime UploadDate { get; set; }
    }
}
