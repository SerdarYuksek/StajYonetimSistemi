using Microsoft.AspNetCore.Identity;

namespace StajYonetimGUI.Models.User
{
    //Identity kütüphanesi entegreli personel ve student tablolarının propertileri
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string TCNO { get; set; }
        public string Role { get; set; }
        public string? PersonalNo { get; set; }
        public string? StudentNo { get; set; }
        public string? Class { get; set; }
        public string? Title { get; set; }
        public bool Gender { get; set; }
        public bool? RegistrationCheck { get; set; }
        public string? Token { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }

    }
}
