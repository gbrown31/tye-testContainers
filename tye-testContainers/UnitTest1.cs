using Docker.DotNet.Models;
using System.ComponentModel;

namespace tye_testContainers
{
    public class ContainerTest : IAsyncLifetime, IDisposable
    {
        private const ushort HttpPort = 80;

        private readonly CancellationTokenSource _cts = new(TimeSpan.FromMinutes(10));

        private readonly INetwork _network;
        private readonly IContainer _dbContainer;
        private readonly IContainer _appContainer;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task DisposeAsync()
        {
            throw new NotImplementedException();
        }

        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Test1()
        {

        }
    }
}