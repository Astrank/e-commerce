using Microsoft.AspNetCore.Mvc;

namespace EPE.UI.ViewComponenets
{
    public class ProductsViewComponent : ViewComponent
    {
        public ProductsViewComponent([FromServices])
        {
        }
        public IViewComponentResult Invoke(string view = "Default")
        {
            return View(view, _getCart.Do());
        }
    }
}