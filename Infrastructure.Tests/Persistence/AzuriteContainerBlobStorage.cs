using Persistence.BlobStorage;

namespace tye_testContainers.Persistence
{
    public class AzuriteContainerBlobStorage : CloudStorage
    {
        public AzuriteContainerBlobStorage(int port) : base(new FileStorageConfig(port))
        {
        }

        public void SeedData()
        {
            StoreProjectFile(new Domain.File("testfile_txt", 10, 1));
        }
    }

    internal class FileStorageConfig : IFileStorageConfig
    {
        private readonly Uri BlobUri;
        private readonly string AccountName;
        private readonly string AccountKey;

        public FileStorageConfig(int port)
        {
            this.BlobUri = new Uri($"http://127.0.0.1:{port}/devstoreaccount1/files");
            this.AccountName = "devstoreaccount1";
            this.AccountKey = "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==";
        }

        public string GetAccountKey()
        {
            return AccountKey;
        }

        public string GetAccountName()
        {
            return AccountName;
        }

        public Uri GetUri()
        {
            return BlobUri;
        }
    }
}
