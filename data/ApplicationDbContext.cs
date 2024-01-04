using Microsoft.EntityFrameworkCore;
using Taskmangaer.models;

namespace Taskmangaer.data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<task> Tasks { get; set; }
        // Other DbSets for your entities

        // Optionally, you can override OnModelCreating to configure entity relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity relationships, keys, constraints, etc.
            base.OnModelCreating(modelBuilder);
        }
    }
}
