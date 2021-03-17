using System.Threading.Tasks;
using EPE.Application.Products;
using Microsoft.AspNetCore.Mvc;

namespace EPE.UI.Controllers
{
    [Route("[controller]")]
    public class StoreController : Controller
    {
        [HttpGet("")]
        public async Task<IActionResult> GetProducts([FromServices] GetProducts getProducts)
        {
            var products = await getProducts.Do();

            return Ok(products);
        }
    }
}