using EPE.Domain.Infrastructure;
using System;
using System.Threading.Tasks;

namespace EPE.Application.ProjectsAdmin
{
    [Service]
    public class UpdateProject
    {
        private readonly IProjectManager _projectManager;

        public UpdateProject(IProjectManager projectManager)
        {
            _projectManager = projectManager;
        }

        public class Request
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Tags { get; set; }
            public string ImagePath { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Tags { get; set; }
            public string ImagePath { get; set; }
        }

        public async Task<Response> Do(Request request)
        {
            var project = _projectManager.GetProjectById(request.Id, x => x);

            await _projectManager.UpdateProject(project);

            return new Response
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Tags = project.Tags,
                ImagePath = project.Image
            };
        }
    }
}
