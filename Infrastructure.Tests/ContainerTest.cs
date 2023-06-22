using Application;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using Microsoft.Data.SqlClient;
using Testcontainers.Azurite;
using Testcontainers.SqlEdge;
using tye_testContainers.Persistence;

namespace Infrastructure.Tests
{
    public class ContainerTest : IAsyncLifetime, IDisposable
    {
        private const int DbPort = 1433;
        private const int BlobPort = 10000;
        private readonly CancellationTokenSource _cts = new(TimeSpan.FromMinutes(5));

        private readonly SqlEdgeContainer _dbContainer;
        private readonly AzuriteContainer _azuriteContainer;

        public ContainerTest()
        {
            //_network = new NetworkBuilder()
            //    .Build();

            _dbContainer = new SqlEdgeBuilder()
                .WithImage("mcr.microsoft.com/azure-sql-edge:1.0.7")
                //.WithNetwork(_network)
                //.WithNetworkAliases("db")
                .WithExposedPort(DbPort)
                .WithEnvironment("ACCEPT_EULA", "Y")
                .WithEnvironment("SA_PASSWORD", "example_123")
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(DbPort))
                .Build();


            _azuriteContainer = new AzuriteBuilder()
                .WithImage("mcr.microsoft.com/azure-storage/azurite")
                //.WithNetwork(_network)
                //.WithNetworkAliases("azurite")
                .WithExposedPort(BlobPort)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(BlobPort))
                .Build();
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync(_cts.Token).ConfigureAwait(false);
            await _azuriteContainer.StartAsync(_cts.Token).ConfigureAwait(false);
        }

        [Fact]
        public void DbContainer_IsHealthy()
        {
            Assert.Equal(TestcontainersStates.Running, _dbContainer.State);
        }

        [Fact]
        public void AzureContainer_IsHealthy()
        {
            Assert.Equal(TestcontainersStates.Running, _azuriteContainer.State);
        }

        [Fact]
        public async Task UserWithProjects_ShouldReturnFiles()
        {
            int port = _dbContainer.GetMappedPublicPort(DbPort);
            var db = new SqlEdgeContainerDatabase(port);
            db.Database.EnsureCreated();
            db.SeedDatabase();

            int storagePort = _azuriteContainer.GetMappedPublicPort(BlobPort);
            var fileStorage = new AzuriteContainerBlobStorage(storagePort);
            //fileStorage.SeedData();

            GetAllProjectFilesQuery query = new GetAllProjectFilesQuery();
            query.UserId = db.ValidUserId;

            GetAllProjectFiles getAllFiles = new GetAllProjectFiles(db, fileStorage);

            var files = await getAllFiles.HandleAsync(query);

            Assert.NotNull(files);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _cts.Dispose();
        }
    }
}