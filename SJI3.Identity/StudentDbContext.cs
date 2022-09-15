using Microsoft.EntityFrameworkCore;

namespace SJI3.Identity
{
    public class StudentDbContext : DbContext
    {
        private readonly string connectionString;

        public StudentDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Student> Student { get; set; }
    }
}