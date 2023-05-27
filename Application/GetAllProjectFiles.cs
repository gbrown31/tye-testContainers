﻿using Application.Persistence;
using File = Domain.File;

namespace Application
{
    public class GetAllProjectFiles
    {
        private readonly IDatabase DbContext;
        private readonly IFileStorage FileStorage;
        public GetAllProjectFiles(IDatabase dbContext, IFileStorage fileStorage)
        {
            this.DbContext = dbContext;
            this.FileStorage = fileStorage;
        }

        public async Task<ICollection<File>> HandleAsync(GetAllProjectFilesQuery query)
        {
            List<File> projectFiles = new List<File>();
            if (query != null)
            {
                // validate query
                ValidateQuery(query);

                // execute db search
                List<File> userFiles = (from a in DbContext.Users
                                        join b in DbContext.ProjectUsers on a.Id equals b.UserId
                                        join c in DbContext.ProjectGroups on b.ProjectId equals c.ProjectId
                                        join d in DbContext.Files on c.ProjectId equals d.ProjectId
                                        where a.Id == query.UserId
                                        select d).ToList();

                projectFiles.AddRange(userFiles);
            }

            return await Task.FromResult<List<File>>(projectFiles);
        }

        private void ValidateQuery(GetAllProjectFilesQuery query)
        {
            if (query.UserId <= 0)
            {
                throw new ArgumentException("invalid user id");
            }
        }
    }
}