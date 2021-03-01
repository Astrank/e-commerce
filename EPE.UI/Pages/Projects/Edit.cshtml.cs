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
    public class EditModel : PageModel
    {
        private IProjectRepository _repository;
        
        public EditModel(IProjectRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public ProjectViewModel Project { get; set; }
        public ProjectViewModel ProjectToUpdate { get; set; }

        public void OnGet(int id)
        {
            ProjectToUpdate = _repository.GetProject(id);
        }

        public async Task<IActionResult> OnPost()
        {
            await _repository.UpdateProject(Project);

            if (await _repository.SaveChangesAsync())
            {
                return RedirectToPage("/Projects/Project", new { id = Project.Id });
            }

            return RedirectToPage("/Projects/Projects");
        }
    }
}
