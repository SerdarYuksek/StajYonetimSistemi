using Microsoft.AspNetCore.Identity;

namespace UserService.Api.Model
{
    //Identity kütüphanesi ile entegreli kullanıcı rol bilgilerinin tutulduğu tablo
    public class AppRole : IdentityRole<int>
    {
    }
}
