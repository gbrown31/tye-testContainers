using Application.Persistence;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Domain;
using System.Collections.Immutable;
using System.Text;

namespace Persistence.BlobStorage
{
    public class CloudStorage : IFileStorage
    {
        private readonly IFileStorageConfig config;
        private readonly BlobContainerClient client;

        public CloudStorage(IFileStorageConfig config)
        {
            this.config = config;
            this.client = new BlobContainerClient(
                config.GetUri(),
                new StorageSharedKeyCredential(config.GetAccountName(), config.GetAccountKey())
            );

            client.CreateIfNotExists();
        }

        public ICollection<Domain.File> RetrieveProjectFiles(Project project)
        {
            //ImmutableList<BlobItem> blobs = client.GetBlobs().

            var blobs = client.GetBlobs();

            return blobs.Select(a => new Domain.File(a.Name, a.Properties.ContentLength.GetValueOrDefault(0), project.Id)).ToImmutableList();
        }

        public bool StoreProjectFile(Domain.File fileToBeStored)
        {
            return true;
        }
        public bool IsHealthy()
        {
            return client.Exists().Value;
        }
    }
}