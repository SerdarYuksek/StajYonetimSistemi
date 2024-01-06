using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserService.Api.Model;

namespace UserService.Api.Context
{
    //User bilgilerinin tutulduğu veritabanına IdentityServer kütüphanesini dahil etme
    public class UserIdentityDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public UserIdentityDbContext(DbContextOptions<UserIdentityDbContext> options) : base(options)
        { }

        //User Servicesindeki Tabloların Veri Tabanına Eklenmesi
        public DbSet<AppUser> appUsers { get; set; }
    }
}
