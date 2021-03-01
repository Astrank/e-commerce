using System.Linq;
using System.Threading.Tasks;
using EPE.Application.ProductsAdmin;
using EPE.Database;
using EPE.Database.FileManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EPE.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class ProductsController : Controller
    {
        private ApplicationDbContext _context;
        private IFileManager _fileManager;

        public ProductsController(ApplicationDbContext context, IFileManager fileManager)
        {
            _context = context;
            _fileManager = fileManager;
        }

        [HttpGet("")]
        public IActionResult GetProducts() => Ok(new GetProducts(_context).Do());

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id) => Ok(new GetProduct(_context).Do(id));

        [HttpPost("")]
        public async Task<IActionResult> CreateProduct(CreateProduct.Request request) =>
            Ok(await new CreateProduct(_context, _fileManager).Do(request));

        [HttpPut("")]
        public async Task<IActionResult> UpdateProduct(UpdateProduct.Request request) => 
            Ok(await new UpdateProduct(_context, _fileManager).Do(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id) => Ok(await new DeleteProduct(_context, _fileManager).Do(id));
    }
}