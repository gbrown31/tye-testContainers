using Persistence.BlobStorage;

namespace WebAPI
{
    internal class FileStorageConfig : IFileStorageConfig
    {
        private const string Host = "FileStorage:Uri";
        private const string Path = "FileStorage:ContainerName";
        private const string Account = "FileStorage:AccountName";
        private const string Key = "FileStorage:Key";

        private readonly Uri BlobUri;
        private readonly string AccountName;
        private readonly string AccountKey;

        public FileStorageConfig(IConfiguration configuration)
        {
            this.BlobUri = new Uri($"{configuration.GetValue<string>(Host)}{configuration.GetValue<string>(Path)}");
            this.AccountName = configuration[Account];
            this.AccountKey = configuration[Key];
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