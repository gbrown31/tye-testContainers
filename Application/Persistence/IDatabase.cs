using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Persistence
{
    public interface IDatabase
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectGroup> ProjectGroups { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<Domain.File> Files { get; set; }

        public bool IsHealthy();
    }
}
