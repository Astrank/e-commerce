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
    public class ProjectsModel : PageModel
    {
        private IProjectRepository _repository;
        
        public ProjectsModel(IProjectRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public ProjectViewModel Project { get; set; }
        public List<ProjectViewModel> Projects { get; set; }

        public void OnGet()
        {
            Projects = _repository.GetAllProjects().ToList();
        }

        public void OnPost()
        {
            
        }
    }
}
