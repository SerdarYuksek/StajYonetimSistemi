using Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Context
{
    public class SurveyDbContext : DbContext
    {
        // Veri tabanı bağlantısını içeren bağlantı adresinin tanımlanması
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=localhost; port=5432; Database=SurveyStajYonetim; User Id=postgres; Password=serdar1907");
        }

        //Tabloların Veri Tabanına Eklenmesi
        public DbSet<SurveyQuestion> surveyQuestions { get; set; }
        public DbSet<SurveyAnswer> surveyAnswers { get; set; }
    }
}
