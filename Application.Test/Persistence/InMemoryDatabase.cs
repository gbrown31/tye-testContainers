using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Application.Test.Persistence
{
    internal class InMemoryDatabase : AppDbContext
    {
        public InMemoryDatabase() : base (GetInMemoryDbContextOptions())
        {
            SeedDatabase();
        }

        private static DbContextOptions GetInMemoryDbContextOptions()
        {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                            .UseInMemoryDatabase(databaseName: myDatabaseName)
                            .Options;
            return options;
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

            Domain.File testFile = new Domain.File("testfile.txt", 10, 1);
            Files.Add(testFile);

            project1.Files.Add(testFile);

            SaveChanges();
        }
    }
}
