using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ProjectGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectGroupId { get; set; }
        public int ProjectId { get; set; }
        public ICollection<ProjectUser> Users { get; set; }
        public ProjectGroup()
        {
            Users = new List<ProjectUser>();
        }
    }
}
