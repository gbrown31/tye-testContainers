using Application;
using DotNet.Testcontainers.Containers;
using tye_testContainers;

namespace Infrastructure.Tests
{
    public class ContainerTest : IClassFixture<ContainerFixture>
    {
        private readonly ContainerFixture _fixture;
        public ContainerTest(ContainerFixture fixture) 
        { 
            _fixture = fixture;
        }

        [Fact]
        public void DbContainer_IsHealthy()
        {
            Assert.Equal(TestcontainersStates.Running, _fixture.DbContainer.State);
        }

        [Fact]
        public void AzureContainer_IsHealthy()
        {
            Assert.Equal(TestcontainersStates.Running, _fixture.BlobContainer.State);
        }

        [Fact]
        public async Task UserWithProjects_ShouldReturnFiles()
        {
            GetAllUserFilesQuery query = new GetAllUserFilesQuery();
            query.UserId = _fixture.DbContext.ValidUserId;

            GetAllUserFiles getAllFiles = new GetAllUserFiles(_fixture.DbContext, _fixture.FileStorage);

            var files = await getAllFiles.HandleAsync(query);

            Assert.NotNull(files);
        }
        [Fact]
        public void AddProjectFile_ShouldReturnSuccess()
        {
            AddFileToProjectCommand command = new AddFileToProjectCommand();
            command.UserId = _fixture.DbContext.ValidUserId;
            command.ProjectId = 10;
            command.FileName = "Test";

            AddFileToProjectHandler handler = new AddFileToProjectHandler(_fixture.DbContext, _fixture.FileStorage);
            var result = handler.Handle(command);

            Assert.True(result);
        }
    }
}