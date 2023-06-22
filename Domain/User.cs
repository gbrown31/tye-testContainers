using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; private set; }
        public ICollection<Project> Projects { get; private set; }

        public User(string username)
        {
            Username = username;
            Projects = new List<Project>();
        }
    }
}