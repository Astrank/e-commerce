using System.Threading.Tasks;
using EPE.Application.Categories;
using EPE.Application.Products;
using Microsoft.AspNetCore.Mvc;

namespace EPE.UI.Controllers
{
    [Route("[controller]/category")]
    public class StoreController : Controller
    {
        /*[HttpGet("")]
        public async Task<IActionResult> GetProducts([FromServices] GetProducts getProducts)
        {
            var products = await getProducts.Do();

            return Ok(products);
        }

        [HttpGet("")]
        public IActionResult GetCategoryWithProducts(
            [FromServices] GetCategoryWithProducts getCategoryWithProducts,
            string category)
        {
            var response = getCategoryWithProducts.Do(category);

            return Ok(response);
        }*/
    }
}