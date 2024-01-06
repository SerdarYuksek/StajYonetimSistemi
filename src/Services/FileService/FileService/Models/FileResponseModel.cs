namespace FileService.Api.Models
{

    //Bir Dosyanın indirme ve görüntüleme işlemleri için dönmesi gereken değerlerin tutulduğu model
    public class FileResponseModel
    {
        public byte[] Bytes { get; set; }
        public string FileName { get; set; }
    }
}
