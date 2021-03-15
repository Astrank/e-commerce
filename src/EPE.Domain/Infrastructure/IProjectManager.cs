using EPE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EPE.Domain.Infrastructure
{
    public interface IProjectManager
    {
        Task<int> CreateProject(Project project);
        TResult GetProjectById<TResult>(int id, Func<Project, TResult> selector);
        TResult GetProjectByName<TResult>(string title, Func<Project, TResult> selector);
        IEnumerable<TResult> GetProjects<TResult>(Func<Project, TResult> selector);
        Task<int> SaveProjectImages(List<ProjectImage> projectImages);
        Task<List<string>> DeleteProject(int id);
        Task<int> UpdateProject(Project project);
    }
}
