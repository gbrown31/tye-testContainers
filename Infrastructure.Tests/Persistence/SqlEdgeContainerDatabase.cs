using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace tye_testContainers.Persistence
{
    public class SqlEdgeContainerDatabase : AppDbContext
    {
        public int ValidUserId
        {
            get;private set;
        }

        public SqlEdgeContainerDatabase(int publicPort) : base(GetContainerOptions(publicPort))
        {
            //SeedDatabase();
        }

        private static DbContextOptions GetContainerOptions(int publicPort)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                            .UseSqlServer($"server=localhost,{publicPort};database=AppDb;user=sa;password=example_123;TrustServerCertificate=True")
                            .Options;
            return options;
        }

        public void SeedDatabase()
        {
            User user1 = new User("test");
            //user1.Id = 1;
            Users.Add(user1);

            SaveChanges();

            Project project1 = new Project("project1");
            //project1.Id = 1;
            user1.Projects.Add(project1);
            Projects.Add(project1);

            SaveChanges();

            ProjectUser projectUser = new ProjectUser();
            projectUser.UserId = user1.Id;
            projectUser.ProjectId = project1.Id;
            ProjectUsers.Add(projectUser);

            SaveChanges();

            ProjectGroup group = new ProjectGroup();
            group.ProjectId = project1.Id;
            group.Users.Add(projectUser);
            ProjectGroups.Add(group);

            SaveChanges();

            Domain.File testFile = new Domain.File("testfile_txt", 10, project1.Id);
            Files.Add(testFile);

            SaveChanges();

            project1.Files.Add(testFile);

            SaveChanges();

            ValidUserId = user1.Id;
        }
    }
}