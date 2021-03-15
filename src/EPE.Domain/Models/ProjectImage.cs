namespace EPE.Domain.Models
{
    public class ProjectImage
    {
        public int Id { get; set; }   
        public string Path { get; set; }  

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}