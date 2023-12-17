using Microsoft.EntityFrameworkCore;
using UserService.Api.Model;

namespace UserService.Api.Context
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        { }
        
        //User Servicesindeki Tabloların Veri Tabanına Eklenmesi
        public DbSet<Student> students { get; set; }
        public DbSet<Personal> personals { get; set; }
    }
}
