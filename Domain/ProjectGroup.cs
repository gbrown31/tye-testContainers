namespace Domain
{
    public class ProjectGroup
    {
        public int ProjectGroupId { get; set; }
        public int ProjectId { get; set; }
        public ICollection<ProjectUser> Users { get; set; }
        public ProjectGroup()
        {
            Users = new List<ProjectUser>();
        }
    }
}
