using EPE.Application.ProjectsAdmin;
using EPE.UI.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EPE.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class ProjectsController : Controller
    {
        private IFileManager _fileManager;

        public ProjectsController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public string rootPath = "ProjectsPath:Images";

        [HttpGet("")]
        public IActionResult GetProjects([FromServices] GetAllProjects getAllProjects) =>
            Ok(getAllProjects.Do());

        [HttpGet("{id}")]
        public IActionResult GetProject([FromServices] GetProject getProject, int id) =>
            Ok(getProject.Do(id));

        [HttpPost("")]
        public IActionResult CreateProject(
            [FromServices] CreateProject createProject, 
            CreateProject.Request request) =>
                Ok(createProject.Do(request));

        [HttpPut("")]
        public IActionResult UpdateProject(
            [FromServices] UpdateProject updateProject,
            UpdateProject.Request request) =>
                Ok(updateProject.Do(request));

        [HttpDelete("{id}/{image}")]
        public async Task<int> DeleteProject([FromServices] DeleteProject deleteProject, int id, string image)
        {
            var success = await deleteProject.Do(id);

            _fileManager.DeleteImage(rootPath, image);

            return success;
        }
    }
}
