using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; private set; } = string.Empty;
        public ICollection<File> Files { get; private set; }

        public Project(string name)
        {
            Name = name;
            Files = new List<File>();
        }
    }
}
