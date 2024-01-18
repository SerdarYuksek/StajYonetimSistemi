namespace FileService.Api.Models
{

    //MongoDB veritabanı bağlantısı için gerekli olan entityler
    //Bu entitylere json dosyasından atamaları yapılıyor
    public class MongoDbSettings
    {
        public string ConnectionUrl { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }

    }
}
