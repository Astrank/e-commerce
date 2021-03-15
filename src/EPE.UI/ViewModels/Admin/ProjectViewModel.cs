using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace EPE.UI.ViewModels
{
    public class ProjectViewModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Tags { get; set; }
            public string PrimaryImage { get; set; }
            public List<string> Images { get; set; }
            
            public IFormFile PrimaryImageFile { get; set; }
            public List<IFormFile> ImageFiles { get; set; }
        }
}