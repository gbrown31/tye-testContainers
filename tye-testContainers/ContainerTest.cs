using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using Testcontainers.PostgreSql;

namespace tye_testContainers
{
    public class ContainerTest : IAsyncLifetime, IDisposable
    {
        private const ushort HttpPort = 80;
        private readonly CancellationTokenSource _cts = new(TimeSpan.FromMinutes(10));

        private readonly INetwork _network;
        private readonly IContainer _dbContainer;
        //private readonly IContainer _appContainer;

        public ContainerTest()
        {
            _network = new NetworkBuilder()
                .Build();

            _dbContainer = new PostgreSqlBuilder()
                .WithImage("postgres")
                .WithNetwork(_network)
                .WithNetworkAliases("db")
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
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
            //await _network.DisposeAsync();
            //await _dbContainer.DisposeAsync();

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _cts.Dispose();
        }
    }
}