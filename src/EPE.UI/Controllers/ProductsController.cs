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

            var imagePath = await _fileManager.SaveImage(rootPath, vm.ImageFile);

            var request = new CreateProduct.Request
            {
                Name = vm.Name,
                Description = vm.Description,
                Value = vm.Value,
                Image = imagePath
            };

            return Ok(createProduct.Do(request));
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
                Image = vm.Image,
            };

            if (vm.ImageFile != null)
            {
                if (vm.Image != null || vm.Image != "")
                {
                    _fileManager.DeleteImage(rootPath, vm.Image);
                }

                var imgPath = await _fileManager.SaveImage(rootPath, vm.ImageFile);
                request.Image = imgPath.ToString();
            };

            return Ok(updateProduct.Do(request));
        } 

        [HttpDelete("{id}/{image}")]
        public async Task<int> DeleteProduct([FromServices] DeleteProduct deleteProduct, int id, string image)
        {
            var success = await deleteProduct.Do(id);
            
            _fileManager.DeleteImage(rootPath, image);

            return success;
        }
    }
}