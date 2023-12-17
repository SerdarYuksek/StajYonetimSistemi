namespace UserService.Api.Dtos
{
    //User Servicenin diğer serviceler ile arasındaki haberleşmede göndereceği Personal entityleri
    public class PersonalDto
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string TCNO { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string PersonalNo { get; set; }
    }
}
