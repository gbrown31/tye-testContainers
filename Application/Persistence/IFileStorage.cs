using Domain;
using File = Domain.File;

namespace Application.Persistence
{
    public interface IFileStorage
    {
        public bool StoreProjectFile(File fileToBeStored);
        public ICollection<File> RetrieveProjectFiles(Project project);
    }
}
