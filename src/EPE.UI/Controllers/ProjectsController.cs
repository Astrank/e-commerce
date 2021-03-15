using EPE.Application.ProjectsAdmin;
using EPE.UI.Infrastructure;
using EPE.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

                var imagePath = await _fileManager.SaveImage(rootPath, vm.PrimaryImageFile);

                var images = new List<string>();

                if (vm.ImageFiles != null)
                {
                    foreach (var image in vm.ImageFiles)
                    {
                        images.Add(await _fileManager.SaveImage(rootPath, image));
                    }
                }

                var request = new CreateProject.Request
                {
                    Title = vm.Title,
                    Description = vm.Description,
                    Tags = vm.Tags,
                    PrimaryImage = imagePath,
                    Images = images
                };

                return Ok(await createProject.Do(request));
            }

        [HttpPut("")]
        public async Task<IActionResult> UpdateProject(
            [FromServices] UpdateProject updateProject,
            ProjectViewModel vm)
            {
                var request = new UpdateProject.Request
                {
                    Id = vm.Id,
                    Title = vm.Title,
                    Description = vm.Description,
                    Tags = vm.Tags,
                    PrimaryImage = vm.PrimaryImage,
                    Images = vm.Images
                };

                // TODO: refactor
                if (vm.PrimaryImageFile != null)
                {
                    if (vm.PrimaryImage != null || vm.PrimaryImage != "")
                    {
                        _fileManager.DeleteImage(rootPath, vm.PrimaryImage);
                    }

                    var imgPath = await _fileManager.SaveImage(rootPath, vm.PrimaryImageFile);
                    request.PrimaryImage = imgPath;
                };

                if (vm.ImageFiles != null)
                {
                    if (vm.Images != null)
                    {
                        foreach (var image in vm.Images)
                        {
                            _fileManager.DeleteImage(rootPath, image);
                        }
                    }

                    List<string> images =  new List<string>();

                    foreach (var image in vm.ImageFiles)
                    {
                        var imgPath = await _fileManager.SaveImage(rootPath, vm.PrimaryImageFile);
                        images.Add(imgPath);
                    }

                    request.Images = images;
                };

                return Ok(await updateProject.Do(request));
            }

        [HttpDelete("{id}/{image}")]
        public async Task DeleteProject([FromServices] DeleteProject deleteProject, int id, string image)
        {
            var images = await deleteProject.Do(id);

            foreach (var i in images)
            {
                _fileManager.DeleteImage(rootPath, i);
            }
        }
    }
}
