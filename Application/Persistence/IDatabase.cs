using Domain;

namespace Application.Persistence
{
    public interface IDatabase
    {
        public ICollection<User> Users { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<ProjectGroup> ProjectGroups { get; set; }
        public ICollection<ProjectUser> ProjectUsers { get; set; }
        public ICollection<Domain.File> Files { get; set; }
    }
}
