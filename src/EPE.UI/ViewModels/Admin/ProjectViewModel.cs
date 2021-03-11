using Microsoft.AspNetCore.Http;

namespace EPE.UI.ViewModels
{
    public class ProjectViewModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Tags { get; set; }
            public string Image { get; set; }
            public IFormFile ImageFile { get; set; }
        }
}