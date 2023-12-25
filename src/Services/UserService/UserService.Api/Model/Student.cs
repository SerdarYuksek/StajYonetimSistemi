using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UserService.Api.Model
{
    //User Servicesindeki Student Tablosunun Identity kütüphanesi ile entegreli Entityleri
    public class Student : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string TCNO { get; set; }
        public string StudentNo { get; set; }
        public bool Gender { get; set; }
        public string Class { get; set; }
        public bool RegistrationCheck { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
     
    }
}
