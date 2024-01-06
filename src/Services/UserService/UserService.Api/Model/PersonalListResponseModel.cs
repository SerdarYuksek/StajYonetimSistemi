namespace UserService.Api.Model
{
    //User Servicesindeki Personal Tablosunun Identity kütüphanesi ile entegreli Entityleri
    public class PersonalListResponseModel
    {
        public string UserName { get; set; }
        public string PersonalNo { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
    }
}

