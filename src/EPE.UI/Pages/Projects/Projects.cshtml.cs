using System.Collections.Generic;
using EPE.Application.Projects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPE.UI.Pages.Projects
{
    public class ProjectsModel : PageModel
    {
        public IEnumerable<GetProjects.Response> Projects { get; set; }

        public void OnGet([FromServices] GetProjects getProducts)
        {
            Projects = getProducts.Do();
        }

        public void OnPost()
        {
            
        }
    }
}
