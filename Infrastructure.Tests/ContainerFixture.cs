using DotNet.Testcontainers.Builders;
using Testcontainers.Azurite;
using Testcontainers.SqlEdge;
using tye_testContainers.Persistence;

namespace tye_testContainers
{
    public class ContainerFixture : IAsyncLifetime
    {
        private readonly CancellationTokenSource _cts = new(TimeSpan.FromMinutes(5));
        public const int DbPort = 1433;
        public const int BlobPort = 10000;

        public readonly SqlEdgeContainer DbContainer;
        public SqlEdgeContainerDatabase DbContext { get; private set; }

        public readonly AzuriteContainer BlobContainer;
        public AzuriteContainerBlobStorage FileStorage { get; private set; }

        public ContainerFixture()
        {
            DbContainer = new SqlEdgeBuilder()
                .WithImage("mcr.microsoft.com/azure-sql-edge:1.0.7")
                //.WithNetwork(_network)
                //.WithNetworkAliases("db")
                .WithExposedPort(DbPort)
                .WithEnvironment("ACCEPT_EULA", "Y")
                .WithEnvironment("SA_PASSWORD", "example_123")
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(DbPort))
                .Build();


            BlobContainer = new AzuriteBuilder()
                .WithImage("mcr.microsoft.com/azure-storage/azurite")
                //.WithNetwork(_network)
                //.WithNetworkAliases("azurite")
                .WithExposedPort(BlobPort)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(BlobPort))
                .Build();
        }

        public async Task InitializeAsync()
        {
            await DbContainer.StartAsync(_cts.Token).ConfigureAwait(false);
            await BlobContainer.StartAsync(_cts.Token).ConfigureAwait(false);


            int port = DbContainer.GetMappedPublicPort(DbPort);
            DbContext = new SqlEdgeContainerDatabase(port);
            DbContext.Database.EnsureCreated();
            DbContext.SeedDatabase();

            int storagePort = BlobContainer.GetMappedPublicPort(BlobPort);
            FileStorage = new AzuriteContainerBlobStorage(storagePort);
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
