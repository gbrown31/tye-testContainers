namespace Domain
{
    public class Project
    {
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
