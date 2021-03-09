using EPE.Domain.Infrastructure;
using EPE.Domain.Models;
using System.Threading.Tasks;

namespace EPE.Application.ProjectsAdmin
{
    [Service]
    public class CreateProject
    {
        private readonly IProjectManager _projectManager;

        public CreateProject(IProjectManager projectManager)
        {
            _projectManager = projectManager;
        }

        public class Request
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Tags { get; set; }
            public string Image { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Tags { get; set; }
            public string Image { get; set; }
        } 

        public async Task<Response> Do(Request request)
        {
            var project = new Project
            {
                Title = request.Title,
                Description = request.Description,
                Tags = request.Tags,
                Image = request.Image
            };

            if (await _projectManager.CreateProject(project) <= 0)
            {
                throw new System.Exception("Failed to create a project"); //TODO: custom exceptions
            };

            return new Response
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Tags = project.Tags,
                Image = project.Image,
            };
        }
    }
}
