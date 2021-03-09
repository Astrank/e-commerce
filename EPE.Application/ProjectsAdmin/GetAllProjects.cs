using EPE.Domain.Infrastructure;
using EPE.Domain.Models;
using System;
using System.Collections.Generic;

namespace EPE.Application.ProjectsAdmin
{
    [Service]
    public class GetAllProjects
    {
        private readonly IProjectManager _projectManager;

        public GetAllProjects(IProjectManager projectManager)
        {
            _projectManager = projectManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public string Title { get; set; }
        }

        public IEnumerable<Response> Do()
        {
            return _projectManager.GetProjects(Projection);
        }

        private static Func<Project, Response> Projection = (project) =>
            new Response
            {
                Id = project.Id,
                Title = project.Title,
            };
    }
}
