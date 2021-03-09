using EPE.Domain.Infrastructure;
using System.Threading.Tasks;

namespace EPE.Application.ProjectsAdmin
{
    [Service]
    public class DeleteProject
    {
        private readonly IProjectManager _projectManager;

        public DeleteProject(IProjectManager projectManager)
        {
            _projectManager = projectManager;
        }

        public Task<int> Do(int id)
        {
            return _projectManager.DeleteProject(id);
        }
    }
}
