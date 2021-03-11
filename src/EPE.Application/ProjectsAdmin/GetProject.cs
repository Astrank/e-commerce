using EPE.Domain.Infrastructure;
using EPE.Domain.Models;
using System;

namespace EPE.Application.ProjectsAdmin
{
    [Service]
    public class GetProject
    {
        private readonly IProjectManager _projectManager;

        public GetProject(IProjectManager projectManager)
        {
            _projectManager = projectManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Tags { get; set; }
            public string ImagePath { get; set; }
        }

        public Response Do(int id) => 
            _projectManager.GetProjectById(id, Projection);

        private static Func<Project, Response> Projection = (project) =>
            new Response
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Tags = project.Tags,
                ImagePath = project.Image
            };
    }
}
