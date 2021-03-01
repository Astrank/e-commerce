using System;

namespace EPE.Domain.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public string Image { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}