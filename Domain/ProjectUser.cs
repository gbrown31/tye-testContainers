using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ProjectUser
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public ProjectUser()
        {
        }
    }
}
