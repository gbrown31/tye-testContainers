using Application.Persistence;
using Application.Test.Persistence;
using Domain;

namespace Application.Test
{
    public class ProjectFileTests
    {
        private readonly IDatabase _database;
        private readonly IFileStorage _fileStorage;
        public ProjectFileTests()
        {
            _database = new InMemoryDatabase();
            _fileStorage = new InMemoryFileStorage();            
        }
        [Fact]
        public async Task UserWithProjects_ShouldReturnFiles()
        {
            GetAllUserFilesQuery query = new GetAllUserFilesQuery();
            query.UserId = 1;

            GetAllUserFiles getAllFiles = new GetAllUserFiles(_database, _fileStorage);

            var files = await getAllFiles.HandleAsync(query);

            Assert.NotNull(files);
        }
    }
}