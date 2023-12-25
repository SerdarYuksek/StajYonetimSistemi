using Microsoft.AspNetCore.Identity;

namespace UserService.Api.Model
{
    //Identity kütüphanesi entegreli personel ve student tablolarının propertileri
    public class AppUser : IdentityUser<int>
    {
        public Personal PersonalData { get; set; }
        public Student StudentData { get; set; }

    }
}
