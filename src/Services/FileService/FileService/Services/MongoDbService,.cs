using FileService.Api.Models;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FileService.Api.Services
{

    //Dosya ile ilgili işlemler için option ayarlamaları ve CRUD fonksiyonları 
    public class MongoDbService
    {
        //ImageInfo modeli kullanılarak oluşturulan nesne
        private readonly IMongoCollection<FileDataInfo> _fileInfoCollection;

        //Oluşturulan nesnenin MongoDbService Constructerında dahil edilmesi
        public MongoDbService(IOptions<MongoDbSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionUrl);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _fileInfoCollection = database.GetCollection<FileDataInfo>(mongoDBSettings.Value.CollectionName);
        }

        //Dosyanın Bilgilerinin Veritabanına Kaydedilmesi
        public void SaveFileInfo(FileDataInfo file)
        {
            _fileInfoCollection.InsertOne(file);
        }

        //Dosyanın Bilgilerinin Veritabanından Getirilmesi
        public FileDataInfo GetFile(string id)
        {
            
            return _fileInfoCollection.Find(p => p.Id.ToString() == id).FirstOrDefault();
        }

        //Tüm Dosya Bilgilerinin Veritabanından Getirilmesi 
        public IEnumerable<FileDataInfo> GetAllFiles()
        {
            return _fileInfoCollection.Find(new BsonDocument()).ToList();
        }

        //Dosyanın Bilgilerinin Veritabanından Silinmesi
        public void DeleteFile(string id)
        {
            _fileInfoCollection.DeleteOne(p => p.Id.ToString() == id);
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

        // Dosya türüne göre dosyaları MongoDB'den sorgula ve döndür
        public List<FileDataInfo> GetFilesByType(int fileTypeNumber)
        {
            return _fileInfoCollection.Find(file => file.FileTypeNumber == fileTypeNumber).ToList();
        }

        // Kullanıcı numarası, stajyer numarası ve dosya numarasına göre dosyayı MongoDB'den sorgula ve döndür
        public List<FileDataInfo> GetFileByUserInternAndNumber(string userNo, int internNumber, int fileNumber)
        {   
            return _fileInfoCollection.Find(file =>
                file.UserNo == userNo &&
                file.InternNumber == internNumber &&
                file.FileNumber == fileNumber
            ).ToList();
        }
    }
}
