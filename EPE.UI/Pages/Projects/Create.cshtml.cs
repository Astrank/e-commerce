using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EPE.Application.ProjectsAdmin;

namespace EPE.UI.Pages.Projects
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public CreateProject.Request Project { get; set; }

        public void OnGet(){}

        public async Task<IActionResult> OnPost(
            [FromServices] CreateProject createProject,
            CreateProject.Request request)
        {
            await createProject.Do(request);

            return RedirectToPage("Projects");
        }

    }
}
