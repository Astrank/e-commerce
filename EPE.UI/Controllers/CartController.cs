using System.Threading.Tasks;
using EPE.Application.Cart;
using EPE.Database;
using Microsoft.AspNetCore.Mvc;

namespace EPE.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class CartController : Controller
    {
        private ApplicationDbContext _ctx;

        public CartController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> AddOne(int stockId) 
        {
            var request = new AddToCart.Request
            {
                StockId = stockId,
                Qty = 1
            };

            var addToCart = new AddToCart(HttpContext.Session, _ctx);

            var success = await addToCart.Do(request);
            
            if (success)
            {
                return Ok("Product added.");
            }

            return BadRequest("Error adding product.");
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> RestOne(int stockId) 
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                Qty = 1
            };

            var removeFromCart = new RemoveFromCart(HttpContext.Session, _ctx);

            var success = await removeFromCart.Do(request);
            
            if (success)
            {
                return Ok("Product removed.");
            }

            return BadRequest("Error removing product.");
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> DeleteProduct(int stockId)
        {
            var deleteAllFromCart = new DeleteAllFromCart(HttpContext.Session, _ctx);

            var success = await deleteAllFromCart.Do(stockId);
            
            if (success)
            {
                return Ok("Product deleted from cart.");
            }

            return BadRequest("Error removing product from cart.");
        }
    }
}