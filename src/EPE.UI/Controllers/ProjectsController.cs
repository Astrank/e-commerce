using EPE.Application.ProjectsAdmin;
using EPE.UI.Infrastructure;
using EPE.UI.ViewModels;
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
        public async Task<IActionResult> CreateProject(
            [FromServices] CreateProject createProject, 
            ProjectViewModel vm)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var imagePath = await _fileManager.SaveImage(rootPath, vm.ImageFile);

                var request = new CreateProject.Request
                {
                    Title = vm.Title,
                    Description = vm.Description,
                    Tags = vm.Tags,
                    Image = imagePath
                };

                return Ok(createProject.Do(request));
            }

        [HttpPut("")]
        public async  Task<IActionResult> UpdateProject(
            [FromServices] UpdateProject updateProject,
            ProjectViewModel vm)
            {
                var imagePath = "";
                
                if (vm.ImageFile != null)
                {
                    imagePath = await _fileManager.SaveImage(rootPath, vm.ImageFile);
                    _fileManager.DeleteImage(rootPath, vm.Image);
                } else {
                    imagePath = vm.Image;
                }

                var request = new UpdateProject.Request
                {
                    Id = vm.Id,
                    Title = vm.Title,
                    Description = vm.Description,
                    Tags = vm.Tags,
                    ImagePath = imagePath
                };

                return Ok(updateProject.Do(request));
            }

        [HttpDelete("{id}/{image}")]
        public async Task<int> DeleteProject([FromServices] DeleteProject deleteProject, int id, string image)
        {
            var success = await deleteProject.Do(id);

            if (success > 0 && image != "undefined")
            { 
                _fileManager.DeleteImage(rootPath, image);
            };

            return success;
        }
    }
}
