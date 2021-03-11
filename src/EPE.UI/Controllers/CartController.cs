using System.Threading.Tasks;
using EPE.Application.Cart;
using Microsoft.AspNetCore.Mvc;

namespace EPE.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class CartController : Controller
    {
        [HttpPost("{stockId}")]
        public async Task<IActionResult> AddOne(int stockId, [FromServices] AddToCart addToCart) 
        {
            var request = new AddToCart.Request
            {
                StockId = stockId,
                Qty = 1
            };

            var success = await addToCart.Do(request);
            
            if (success)
            {
                return Ok("Product added.");
            }

            return BadRequest("Error adding product.");
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> RestOne(int stockId, [FromServices] RemoveFromCart removeFromCart) 
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                Qty = 1
            };

            var success = await removeFromCart.Do(request);
            
            if (success)
            {
                return Ok("Product removed.");
            }

            return BadRequest("Error removing product.");
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> DeleteProduct(int stockId, [FromServices] DeleteAllFromCart deleteAllFromCart)
        {
            var request = new DeleteAllFromCart.Request
            {
                StockId = stockId,
            };

            var success = await deleteAllFromCart.Do(request);
            
            if (success)
            {
                return Ok("Product deleted from cart.");
            }

            return BadRequest("Error removing product from cart.");
        }
    }
}