using System.Threading.Tasks;
using EPE.Application.ProductsAdmin;
using EPE.UI.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        public class Asd
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public IFormFile Image { get; set; }
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateProduct([FromServices] CreateProduct createProduct, Asd request)
        {
            var imagePath = await _fileManager.SaveImage(rootPath, request.Image);

            var vm = new CreateProduct.Request
            {
                Name = request.Name,
                Description = request.Description,
                Value = request.Value,
                Image = imagePath
            };

            return Ok(createProduct.Do(vm));
        }

        public class Dsa
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public string Image { get; set; }

            public IFormFile ImageFile { get; set; }
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateProduct([FromServices] UpdateProduct updateProduct, Dsa request)
        {
            var vm = new UpdateProduct.Request
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Value = request.Value,
                Image = request.Image,
            };

            if (request.ImageFile != null)
            {
                if (request.Image != null || request.Image != "")
                {
                    _fileManager.DeleteImage(rootPath, request.Image);
                }

                var imgPath = await _fileManager.SaveImage(rootPath, request.ImageFile);
                vm.Image = imgPath.ToString();
            };

            return Ok(updateProduct.Do(vm));
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