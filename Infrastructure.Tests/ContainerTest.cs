using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using Testcontainers.PostgreSql;
//using TestEnvironment.Docker;

namespace Infrastructure.Tests
{
    public class ContainerTest : IAsyncLifetime, IDisposable
    {
        private const int DbPort = 5432;
        private const int BlobPort = 10000;
        private readonly CancellationTokenSource _cts = new(TimeSpan.FromMinutes(5));

        private readonly INetwork _network;
        private readonly IContainer _dbContainer;
        private readonly IContainer _azuriteContainer;

        public ContainerTest()
        {
            _network = new NetworkBuilder()
                .Build();

            _dbContainer = new PostgreSqlBuilder()
                .WithImage("postgres")
                .WithNetwork(_network)
                .WithNetworkAliases("db")
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(DbPort))
                .WithVolumeMount("postgres-data", "/var/lib/postgresql/data")
                .Build();


            _azuriteContainer = new ContainerBuilder()
                .WithImage("mcr.microsoft.com/azure-storage/azurite")
                .WithNetwork(_network)
                .WithNetworkAliases("azurite")
                //.WithCommand("azurite-blob", "--blohost 0.0.0.0", $"--blobPort {BlobPort}")
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(BlobPort))
                .Build();
        }

        public async Task InitializeAsync()
        {
            await _network.CreateAsync(_cts.Token).ConfigureAwait(false);
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