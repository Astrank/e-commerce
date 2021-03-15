using EPE.Domain.Infrastructure;
using EPE.Domain.Models;
using System;
using System.Collections.Generic;

namespace EPE.Application.Projects
{
    [Service]
    public class GetProjects
    {
        private readonly IProjectManager _projectManager;

        public GetProjects(IProjectManager projectManager)
        {
            _projectManager = projectManager;
        }

        public class Response
        {
            public string Title { get; set; }
            public string Desctription { get; set; }
            public string Tags { get; set; }
            public string PrimaryImage { get; set; }
        }

        public IEnumerable<Response> Do()
        {
            var asd = _projectManager.GetProjects(Projection);

            return asd;
        }

        private static Func<Project, Response> Projection = (project) =>
            new Response
            {
                Title = project.Title,
                Desctription = project.Description,
                Tags = project.Tags,
                PrimaryImage = project.PrimaryImage
            };
    }
}
