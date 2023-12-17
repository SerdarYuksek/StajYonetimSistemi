using InternService.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace InternService.Api.Context
{
    public class InternDbContext : DbContext
    {
        public InternDbContext(DbContextOptions<InternDbContext> options) : base(options)
        { }

        //Intern Servicesindeki Tabloların Veri Tabanına Eklenmesi
        public DbSet<InternInfo> internInfos { get; set; }
        public DbSet<InternStatus> internStatuses { get; set; }
    }
}
