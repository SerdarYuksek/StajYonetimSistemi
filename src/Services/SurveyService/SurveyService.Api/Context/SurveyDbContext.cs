namespace SurveyService.Api.Context
{
    public class SurveyDbContext : DbContext
    {
        public SurveyDbContext(DbContextOptions<SurveyDbContext> options) : base(options)
        { }

    }
}
