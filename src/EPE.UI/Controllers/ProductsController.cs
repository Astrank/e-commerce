using System.Collections.Generic;
using System.Threading.Tasks;
using EPE.Application.ProductsAdmin;
using EPE.UI.Infrastructure;
using EPE.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EPE.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class ProductsController : Controller
    {
        private IFileManager _fileManager;

        public ProductsController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public string rootPath = "ProductsPath:Images";

        [HttpGet("")]
        public IActionResult GetProducts([FromServices] GetProducts getProducts)
        {
            var products = getProducts.Do();
            
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct([FromServices] GetProduct getProduct, int id) => 
            Ok(getProduct.Do(id));

        [HttpPost("")]
        public async Task<IActionResult> CreateProduct(
            [FromServices] CreateProduct createProduct, 
            ProductViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var primaryImage = await _fileManager.SaveImage(rootPath, vm.PrimaryImageFile);
            
            var images = new List<string>();

            if (vm.ImageFiles != null)
            {
                foreach (var image in vm.ImageFiles)
                {
                    images.Add(await _fileManager.SaveImage(rootPath, image));
                }
            }

            var request = new CreateProduct.Request
            {
                Name = vm.Name,
                Description = vm.Description,
                Value = vm.Value,
                PrimaryImage = primaryImage,
                Images = images,
                CategoryId = vm.CategoryId
            };

            return Ok(await createProduct.Do(request));
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateProduct(
            [FromServices] UpdateProduct updateProduct, 
            ProductViewModel vm)
        {
            var request = new UpdateProduct.Request
            {
                Id = vm.Id,
                Name = vm.Name,
                Description = vm.Description,
                Value = vm.Value,
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
                    var imgPath = await _fileManager.SaveImage(rootPath, image);
                    images.Add(imgPath);
                }

                request.Images = images;
            };

            return Ok(await updateProduct.Do(request));
        } 

        [HttpDelete("{id}")]
        public async Task DeleteProduct([FromServices] DeleteProduct deleteProduct, int id)
        {
            var images = await deleteProduct.Do(id);

            foreach (var i in images)
            {
                _fileManager.DeleteImage(rootPath, i);
            }
        }
    }
}