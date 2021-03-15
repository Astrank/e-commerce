using EPE.Domain.Infrastructure;
using EPE.Domain.Models;
using System;
using System.Collections.Generic;

namespace EPE.Application.Projects
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
            public string Title { get; set; }
            public string Description { get; set; }
            public string Tags { get; set; }
            public string ImagePath { get; set; }
        }

        public Response Do(string name)
        {
            return _projectManager.GetProjectByName(name, Projection);
        }

        private static Func<Project, Response> Projection = (project) =>
            new Response
            {
                Title = project.Title,
                Description = project.Description,
                Tags = project.Tags,
                ImagePath = project.PrimaryImage
            };
    }
}
