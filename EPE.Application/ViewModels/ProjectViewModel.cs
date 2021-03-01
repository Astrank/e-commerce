using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EPE.Application.ViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Tags { get; set; }
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }
    }
}