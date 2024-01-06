using ImageService.Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ImageService.Api.Services
{
    //Resimler ile ilgili işlemler için İnterface
    public interface IMongoDbService
    {
        void DeletePhoto(string id);
        IEnumerable<ImageInfo> GetAllPhotos();
        ImageInfo GetPhoto(string id);
        void SavePhotoInfo(ImageInfo ımage);
    }

    //Resim ile ilgili işlemler için option ayarlamaları ve CRUD fonksiyonları 
    public class MongoDbService : IMongoDbService
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

    }
}
