namespace UserService.Api.Model
{
    //User Servicesindeki Student Tablosunun Identity kütüphanesi ile entegreli Entityleri
    public class StudentListResponseModel
    {
        public string UserName { get; set; }
        public string StudentNo { get; set; }
        public string Email { get; set; }
    }
}
