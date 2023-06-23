namespace Application
{
    public class AddFileToProjectCommand
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; }
    }
}
