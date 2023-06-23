using Application.Persistence;

namespace Application
{
    public class AddFileToProjectHandler
    {
        private readonly IDatabase DbContext;
        private readonly IFileStorage FileStorage;
        public AddFileToProjectHandler(IDatabase dbContext, IFileStorage fileStorage)
        {
            DbContext = dbContext;
            FileStorage = fileStorage;
        }

        public bool Handle(AddFileToProjectCommand command)
        {
            bool result = false;

            try
            {
                // validate command
                ValidateCommand(command);
                // check user access

                // store file data
                // persist file
                FileStorage.StoreProjectFile(new Domain.File(command.FileName, 10, command.ProjectId));
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        private void ValidateCommand(AddFileToProjectCommand command)
        {
            if (command.UserId <= 0)
            {
                throw new ArgumentException("invalid user id");
            }
            if (command.ProjectId <= 0)
            {
                throw new ArgumentException("invalid project id");
            }
            if (string.IsNullOrEmpty(command.FileName))
            {
                throw new ArgumentException("invalid file name");
            }
        }
    }
}
