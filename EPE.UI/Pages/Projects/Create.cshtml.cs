using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPE.Application.Projects;
using EPE.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPE.UI.Pages.Projects
{
    public class CreateModel : PageModel
    {
        private readonly IProjectRepository _repository;

        public CreateModel(IProjectRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public ProjectViewModel Project { get; set; }

        public void OnGet(){}

        public async Task<IActionResult> OnPost()
        {
            await _repository.CreateProject(Project);
            
            if (await _repository.SaveChangesAsync())
            {
                return RedirectToPage("Projects");
            }
            
            return Page();
        }

    }
}
