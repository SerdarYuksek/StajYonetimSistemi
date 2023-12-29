using ImageService.Api.Models;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ImageService.Api.Services
{

    //Resim ile ilgili işlemler için option ayarlamaları ve CRUD fonksiyonları 
    public class MongoDbService
    {
       //ImageInfo modeli kullanılarak oluşturulan nesne
        private readonly IMongoCollection<ImageInfo> _ımageInfoCollection;

        //Oluşturulan nesnenin MongoDbService Constructerında dahil edilmesi
        public MongoDbService(IOptions<MongoDbSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionUrl);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _ımageInfoCollection = database.GetCollection<ImageInfo>(mongoDBSettings.Value.CollectionName);
        }

        //Resim Bilgilerinin Veritabanına Kaydedilmesi
        public void SavePhotoInfo(ImageInfo ımage)
        {
            _ımageInfoCollection.InsertOne(ımage);
        }

        //Resim Bilgilerinin Veritabanından Getirilmesi
        public ImageInfo GetPhoto(string id)
        {
            return _ımageInfoCollection.Find(p => p.Id == id).FirstOrDefault();
        }

        //Tüm Resim Bilgilerinin Veritabanından Getirilmesi
        public IEnumerable<ImageInfo> GetAllPhotos()
        {
            return _ımageInfoCollection.Find(new BsonDocument()).ToList();
        }

        //Resim Bilgilerinin Veritabanından Silinmesi
        public void DeletePhoto(string id)
        {
            _ımageInfoCollection.DeleteOne(p => p.Id == id);
        }

        // Dosya uzantısına göre MIME türünü belirle
        public string GetContentType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
