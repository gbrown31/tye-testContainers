namespace Domain
{
    public class User
    {
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