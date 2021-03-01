using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPE.Application.ViewModels;
using EPE.Database;
using EPE.Database.FileManager;
using EPE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EPE.Application.Projects
{
    public class SQLServer_ProjectRepository : IProjectRepository
    {
        private ApplicationDbContext _context;
        private IFileManager _fileManager;
        private IConfiguration _config;

        public SQLServer_ProjectRepository(ApplicationDbContext context, IFileManager fileManager, IConfiguration config)
        {
            _fileManager = fileManager;
            _context = context;
            _config = config;
        }

        public async Task CreateProject(ProjectViewModel vm)
        {
            var img = await _fileManager.SaveImage(_config["ProjectsPath:Images"], vm.Image);
            var project = new Project()
            {
                Title = vm.Title,
                Description = vm.Description,
                Tags = vm.Tags,
                Image = img
            };
            _context.Projects.Add(project);
        }

        public ProjectViewModel GetProject(int id)
        {
            var project = _context.Projects.AsNoTracking().FirstOrDefault(x => x.Id == id);
            var vm = new ProjectViewModel
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Tags = project.Tags,
                ImagePath = project.Image
            };
            return vm;
        }

        public IEnumerable<ProjectViewModel> GetAllProjects()
        {
            return _context.Projects.ToList().Select(x => new ProjectViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Tags = x.Tags,
                ImagePath = x.Image
            });
        }

        public async Task<bool> DeleteProject(int id)
        {
            var vm = GetProject(id);
            var project = new Project()
            {
                Id = vm.Id,
                Title = vm.Title,
                Description = vm.Description,
                Tags = vm.Tags,
                Image = vm.ImagePath
            };
            _context.Projects.Remove(project);

            if (await SaveChangesAsync())
            {
                if (project.Image != null)
                {
                    _fileManager.DeleteImage(_config["ProjectsPath:Images"], project.Image);
                }
                return true;
            }
            return false;
        }

        public async Task UpdateProject(ProjectViewModel vm)
        {
            var project = new Project()
            {
                Id = vm.Id,
                Title = vm.Title,
                Description = vm.Description,
                Tags = vm.Tags
            };

            if (vm.Image != null && vm.ImagePath != null)
            {
                _fileManager.DeleteImage(_config["ProjectsPath:Images"], vm.ImagePath);
                project.Image = await _fileManager.SaveImage(_config["ProjectsPath:Images"], vm.Image);
            }
            else if (vm.Image == null && vm.ImagePath != null)
            {
                project.Image = vm.ImagePath;
            }

            //TODO: Error

            _context.Projects.Update(project);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
    }
}