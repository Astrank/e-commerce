using EPE.Application.Cart;
using EPE.Database;
using Microsoft.AspNetCore.Mvc;

namespace EPE.UI.ViewComponenets
{
    public class CartViewComponent : ViewComponent
    {
        private GetCart _getCart;
        public CartViewComponent([FromServices] GetCart getCart)
        {
            _getCart = getCart;
        }
        public IViewComponentResult Invoke(string view = "Default")
        {
            return View(view, _getCart.Do());
        }
    }
}