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
    public class ProjectModel : PageModel
    {
        private IProjectRepository _repository;
        public ProjectModel(IProjectRepository repository)
        {
            _repository = repository;
        }

        public ProjectViewModel Project { get; set; }

        public void OnGet(int id)
        {
            Project = _repository.GetProject(id);
        }

        public async Task<IActionResult> OnPost(int id)
        {
            bool isValid = await _repository.DeleteProject(id);
            if (isValid)
            {
                return RedirectToPage("/Projects/Projects");
            }            
            return RedirectToPage("/Index");
        }
    }
}
