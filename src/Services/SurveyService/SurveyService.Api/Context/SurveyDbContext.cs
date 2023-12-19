using Microsoft.EntityFrameworkCore;
using SurveyService.Api.Model;

namespace SurveyService.Api.Context
{
    public class SurveyDbContext : DbContext
    {
        public SurveyDbContext(DbContextOptions<SurveyDbContext> options) : base(options)
        { }

        //Survey Servicesindeki Tabloların Veri Tabanına Eklenmesi
        public DbSet<SurveyQuestion> surveyQuestions { get; set; }
        public DbSet<SurveyAnswer> surveyAnswers { get; set; }
    }
}
