using Application.Persistence;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test.Persistence
{
    internal class InMemoryFileStorage : IFileStorage
    {
        public ICollection<Domain.File> RetrieveProjectFiles(Project project)
        {
            throw new NotImplementedException();
        }

        public bool StoreProjectFile(Domain.File fileToBeStored)
        {
            throw new NotImplementedException();
        }
    }
}
