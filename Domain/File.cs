namespace Domain
{
    public class File
    {
        public int Id { get; private set; }
        public int ProjectId { get; private set; }
        public string Name { get; private set; }
        public int FileSize { get; private set; }
        public string Extension { get; private set; }

        public File(int id, string name, int fileSize, int projectId)
        {
            Id = id;
            Name = name;
            FileSize = fileSize;
            ProjectId = projectId;
            FileInfo fileInfo = new FileInfo(name);
            Extension = fileInfo.Extension;            
        }
    }
}
