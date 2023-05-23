using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using Testcontainers.PostgreSql;

namespace tye_testContainers
{
    public class ContainerTest : IAsyncLifetime, IDisposable
    {
        private const int DbPort = 5432;
        private readonly CancellationTokenSource _cts = new(TimeSpan.FromMinutes(0.5));

        private readonly INetwork _network;
        private readonly IContainer _dbContainer;

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
        }

        public async Task InitializeAsync()
        {
            await _network.CreateAsync(_cts.Token).ConfigureAwait(false);

            await _dbContainer.StartAsync(_cts.Token).ConfigureAwait(false);
        }

        [Fact]
        public void DbContainer_IsHealthy()
        {
            Assert.Equal(TestcontainersStates.Running, _dbContainer.State);
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