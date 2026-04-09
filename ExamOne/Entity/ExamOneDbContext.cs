using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExamOne.Entity
{
    public class ExamOneDbContext : IdentityDbContext
    {
        public ExamOneDbContext(DbContextOptions<ExamOneDbContext> options)
            : base(options)
        {
        }
        public DbSet<QuestionBank> QuestionBanks { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamScore> ExamScores { get; set; }
        public DbSet<Branch> Branches { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedData.SeedDataDb(builder);
        }
    }
}
