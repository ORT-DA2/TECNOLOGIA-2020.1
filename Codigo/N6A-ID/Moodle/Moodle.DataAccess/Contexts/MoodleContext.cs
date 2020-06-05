using Microsoft.EntityFrameworkCore;
using Moodle.Domain;

namespace Moodle.DataAccess
{
    public class MoodleContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentsCourses { get; set; }

        public MoodleContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"ConnectionString");
                optionsBuilder.UseLazyLoadingProxies();
            }
        }
    }
}