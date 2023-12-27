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

        public MongoDbService(IOptions<MongoDbSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionUrl);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _ımageInfoCollection = database.GetCollection<ImageInfo>(mongoDBSettings.Value.CollectionName);
        }

        public void SavePhotoInfo(ImageInfo ımage)
        {
            _ımageInfoCollection.InsertOne(ımage);
        }

        public ImageInfo GetPhoto(string id)
        {
            return _ımageInfoCollection.Find(p => p.Id == id).FirstOrDefault();
        }

        public IEnumerable<ImageInfo> GetAllPhotos()
        {
            return _ımageInfoCollection.Find(new BsonDocument()).ToList();
        }

        public void DeletePhoto(string id)
        {
            _ımageInfoCollection.DeleteOne(p => p.Id == id);
        }

        public string GetContentType(string fileName)
        {
            // Dosya uzantısına göre MIME türünü belirle
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
