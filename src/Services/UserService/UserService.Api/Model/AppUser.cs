using Microsoft.AspNetCore.Identity;

namespace UserService.Api.Model
{
    public class AppUser : IdentityUser<int>
    {
        public Personal PersonalData { get; set; }
        public Student StudentData { get; set; }

    }
}
