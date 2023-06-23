namespace Persistence.BlobStorage
{
    public interface IFileStorageConfig
    {
        string GetAccountKey();
        string GetAccountName();
        Uri GetUri();
    }
}