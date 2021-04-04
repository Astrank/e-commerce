using System.Threading.Tasks;
using EPE.Application.Categories;
using EPE.Application.Products;
using Microsoft.AspNetCore.Mvc;

namespace EPE.UI.Controllers
{
    [Route("[controller]")]
    public class StoreController : Controller
    {
        [HttpGet("vue/products/{name}")]
        public IActionResult GetProducts(
            [FromServices] GetProductsByCategoryName getProductsByCategoryName,
            string name)
        {
            var products = getProductsByCategoryName.Do(name);

            return Ok(products);
        }

        [HttpGet("vue/categories/{name}")]
        public IActionResult GetCategory([FromServices] GetCategory getCategory, string name)
        {
            var categories = getCategory.Do(name);

            return Ok(categories);
        }
    }
}