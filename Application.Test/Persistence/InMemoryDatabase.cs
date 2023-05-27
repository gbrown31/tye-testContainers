using Application.Persistence;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test.Persistence
{
    internal class InMemoryDatabase : IDatabase
    {
        public ICollection<User> Users { get; set; } = new HashSet<User>();
        public ICollection<Project> Projects { get; set; } = new HashSet<Project>();
        public ICollection<ProjectGroup> ProjectGroups { get; set; } = new HashSet<ProjectGroup>();
        public ICollection<ProjectUser> ProjectUsers { get; set; } = new HashSet<ProjectUser>();
        public ICollection<Domain.File> Files { get; set; } = new HashSet<Domain.File>();
        public InMemoryDatabase()
        {
            SeedDatabase();
        }

        public void SeedDatabase()
        {
            User user1 = new User("test");
            user1.Id = 1;
            Users.Add(user1);

            Project project1 = new Project("project1");
            project1.Id = 1;
            user1.Projects.Add(project1);
            Projects.Add(project1);

            ProjectUser projectUser = new ProjectUser();
            projectUser.UserId = 1;
            projectUser.ProjectId = 1;
            ProjectUsers.Add(projectUser);

            ProjectGroup group = new ProjectGroup();
            group.ProjectGroupId = 1;
            group.ProjectId = 1;
            group.Users.Add(projectUser);
            ProjectGroups.Add(group);

            Domain.File testFile = new Domain.File(1, "testfile.txt", 10, 1);
            Files.Add(testFile);

            project1.Files.Add(testFile);
        }
    }
}
