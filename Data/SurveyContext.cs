using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using CustomerSurvey.Data.Entities;

namespace CustomerSurvey.Data
{
    public class SurveyContext : DbContext
    {
        public SurveyContext(DbContextOptions<SurveyContext> contextOptions) : base(contextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.CustomerSurvey>()
              .HasMany<CustomerSurveyAnswer>(g => g.CustomerSurveyAnswers).WithOne(s => s.CustomerSurvey)
               .HasForeignKey(s => s.SurveyId);
            modelBuilder.Entity<CustomerSurveyAnswer>().HasKey(s => new { s.Id, s.SurveyId });
        }

        public virtual DbSet<Entities.CustomerSurvey> CustomerSurveys { get; set; }
        public virtual DbSet<Entities.CustomerSurveyAnswer> CustomerSurveyAnswers { get; set; }
        public virtual DbSet<Entities.CustomerSurveyQuestion> CustomerSurveyQuestions { get; set; }
        public virtual DbSet<Entities.UnsubscribeEmail> UnsubscribeEmails { get; set; }
    }
}
