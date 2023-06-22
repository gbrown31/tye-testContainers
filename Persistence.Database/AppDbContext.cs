using Application.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Database
{
    public class AppDbContext : DbContext, IDatabase
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectGroup> ProjectGroups { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<Domain.File> Files { get; set; }
        public AppDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectUser>().HasKey(a => new { a.ProjectId, a.UserId });

            base.OnModelCreating(modelBuilder);
        }

        public bool IsHealthy()
        {
            return base.Database.CanConnect();
        }
    }
}