using System.Collections.Generic;
using System.Threading.Tasks;
using EPE.Application.ViewModels;
using EPE.Domain.Models;

namespace EPE.Application.Projects
{
    public interface IProjectRepository
    {
        Task CreateProject(ProjectViewModel project);
        ProjectViewModel GetProject(int id);
        IEnumerable<ProjectViewModel> GetAllProjects();
        Task<bool> DeleteProject(int id);
        Task UpdateProject(ProjectViewModel project);
        Task<bool> SaveChangesAsync();
    }
}