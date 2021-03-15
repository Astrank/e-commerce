using EPE.Domain.Infrastructure;
using EPE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPE.Database
{
    public class ProjectManager : IProjectManager
    {
        private readonly ApplicationDbContext _ctx;

        public ProjectManager(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public Task<int> CreateProject(Project project)
        {
            _ctx.Projects.Add(project);

            return _ctx.SaveChangesAsync();
        }

        public TResult GetProjectById<TResult>(int id, Func<Project, TResult> selector)
        {
            return _ctx.Projects
                .Include(x => x.Images)
                .Where(x => x.Id == id)
                .Select(selector)
                .FirstOrDefault();
        }
        
        public TResult GetProjectByName<TResult>(string title, Func<Project, TResult> selector)
        {
            return _ctx.Projects
                .Where(x => x.Title == title)
                .Select(selector)
                .FirstOrDefault();
        }

        public IEnumerable<TResult> GetProjects<TResult>(Func<Project, TResult> selector)
        {
            return _ctx.Projects
                .Select(selector)
                .ToList();
        }

        public Task<int> SaveProjectImages(List<ProjectImage> projectImages)
        {
            _ctx.ProjectImage.AddRange(projectImages);

            return _ctx.SaveChangesAsync();
        }

        public Task<int> UpdateProject(Project project)
        {
            _ctx.Projects.Update(project);

            return _ctx.SaveChangesAsync();
        }

        public async Task<List<string>> DeleteProject(int id)
        {
            var project = _ctx.Projects
                .Include(x => x.Images)
                .FirstOrDefault(x => x.Id == id);

            _ctx.Projects.Remove(project);

            if (await _ctx.SaveChangesAsync() > 0)
            {
                var images = new List<string>
                {
                    project.PrimaryImage
                };

                foreach (var image in project.Images)
                {
                    images.Add(image.Path);   
                }

                return images;
            }

            throw new System.Exception("Error deleting a project");
        }
    }
}
