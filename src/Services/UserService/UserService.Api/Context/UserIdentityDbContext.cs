using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserService.Api.Model;

namespace UserService.Api.Context
{
    public class UserIdentityDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public UserIdentityDbContext(DbContextOptions<UserIdentityDbContext> options) : base(options)
        { }
        
        //User Servicesindeki Tabloların Veri Tabanına Eklenmesi
        public DbSet<Student> students { get; set; }
        public DbSet<Personal> personals { get; set; }
    }
}
