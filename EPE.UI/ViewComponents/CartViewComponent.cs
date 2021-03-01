using EPE.Application.Cart;
using EPE.Database;
using Microsoft.AspNetCore.Mvc;

namespace EPE.UI.ViewComponenets
{
    public class CartViewComponent : ViewComponent
    {
        private ApplicationDbContext _ctx;
        public CartViewComponent(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public IViewComponentResult Invoke(string view = "Default")
        {
            return View(view, new GetCart(HttpContext.Session, _ctx).Do());
        }
    }
}