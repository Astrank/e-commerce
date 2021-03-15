using EPE.Domain.Infrastructure;
using EPE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            public string PrimaryImage { get; set; }
            public IEnumerable<string> Images { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Tags { get; set; }
            public string PrimaryImage { get; set; }
            public IEnumerable<string> Images { get; set; }
        }

        public async Task<Response> Do(Request request)
        {
            var project = _projectManager.GetProjectById(request.Id, x => x);

            project.Title = request.Title;
            project.Description = request.Description;
            project.Tags = request.Tags;
            project.PrimaryImage = request.PrimaryImage;

            List<ProjectImage> images = new List<ProjectImage>();

            foreach (var image in request.Images)
            {
                images.Add(new ProjectImage
                {
                    ProjectId = project.Id,
                    Path = image,
                });
            };

            project.Images = images;

            await _projectManager.UpdateProject(project);

            return new Response
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Tags = project.Tags,
                PrimaryImage = project.PrimaryImage,
                Images = project.Images.Select(x => x.Path)
            };
        }
    }
}
