using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPE.Application.Projects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPE.UI.Pages.Projects
{
    public class ProjectModel : PageModel
    {
        public GetProject.Response Project { get; set; }

        public void OnGet([FromServices] GetProject getProject, string title)
        {
            Project = getProject.Do(title);
        }
    }
}
