using Microsoft.EntityFrameworkCore;

namespace CourseEvaluationSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CourseEvaluationSystem.Models.Course> Courses { get; set; } = null!;
        public DbSet<CourseEvaluationSystem.Models.Evaluation> Evaluations { get; set; } = null!;
        public DbSet<CourseEvaluationSystem.Models.Student> Students { get; set; } = null!;
    }
}
