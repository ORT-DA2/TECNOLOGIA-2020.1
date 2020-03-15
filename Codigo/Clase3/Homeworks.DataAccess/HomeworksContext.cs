using Microsoft.EntityFrameworkCore;
using Homeworks.Domain;
namespace Homeworks.DataAccess
{
    public class HomeworksContext : DbContext
    {
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Exercise> Exercises { get; set; }

        public HomeworksContext(DbContextOptions options) : base(options) { }
    }
}