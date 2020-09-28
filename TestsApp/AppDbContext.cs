using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestsApp.Models;
using TestsApp.Models.Test;
using TestsApp.Models.Test.Answers;
using TestsApp.Models.Test.Questions;
using TestsApp.Models.Users;

namespace TestsApp
{
    public class AppDbContext : DbContext
    {
        // All users are stored in one table
        public DbSet<AppUser> Users { get; set; }
        public DbSet<AdminUser> Admins { get; set; }
        public DbSet<TeacherUser> Teachers { get; set; }
        public DbSet<StudentUser> Students { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionWithAnswerVariants> QuestionsWithAnswerVariants { get; set; }
        public DbSet<OneChoiceQuestion> OneChoiceQuestions { get; set; }
        public DbSet<MultiChoiceQuestion> MultiChoiceQuestions { get; set; }
        public DbSet<TwoColumnsQuestion> TwoColumnsQuestions { get; set; }
        public DbSet<OpenAnswerQuestion> OpenAnswerQuestions { get; set; }

        public DbSet<TestResult> TestResults { get; set; }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<MultiChoiceAnswer> MultiChoiceAnswers { get; set; }
        public DbSet<OneChoiceAnswer> OneChoiceAnswers { get; set; }
        public DbSet<TwoColumnsAnswer> TwoColumnsAnswers { get; set; }
        public DbSet<OpenAnswerAnswer> OpenAnswerAnswers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Question>()
                .HasKey(q => new {q.TestId, Order = q.Number});

            modelBuilder.Entity<Answer>()
                .HasKey(a => new {a.TestResultId, a.QuestionNumber});
        }

        public static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();

                if (!dbContext.Users.Any())
                {
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                    ExampleData.FillDatabase(dbContext, userManager);
                }
            }
        }
    }
}
