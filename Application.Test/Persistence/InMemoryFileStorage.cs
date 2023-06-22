using Application.Persistence;
using Domain;

namespace Application.Test.Persistence
{
    internal class InMemoryFileStorage : IFileStorage
    {
        public bool IsHealthy()
        {
            return true;
        }

        public ICollection<Domain.File> RetrieveProjectFiles(Project project)
        {
            return new List<Domain.File>()
            {
                new Domain.File("testfile.txt", 10, 1)
            };
        }

        public bool StoreProjectFile(Domain.File fileToBeStored)
        {
            return true;
        }
    }
}
