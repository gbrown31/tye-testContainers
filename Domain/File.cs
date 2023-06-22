using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class File
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        public int ProjectId { get; private set; }
        public string Name { get; private set; }
        public long FileSize { get; private set; }
        public string Extension { get; private set; }

        public File(string name, long fileSize, int projectId)
        {
            Name = name;
            FileSize = fileSize;
            ProjectId = projectId;
            FileInfo fileInfo = new FileInfo(name);
            Extension = fileInfo.Extension;            
        }
    }
}
