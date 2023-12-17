using System.ComponentModel.DataAnnotations;

namespace UserService.Api.Model
{
    //User Servicesindeki Personal Tablosunun Entityleri
    public class Personal
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string TCNO { get; set; }
        public bool Gender{ get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public string Role { get; set; }
        public string PersonalNo { get; set; }
        public string Title { get; set; }
    }
}

