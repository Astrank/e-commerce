using System.Collections.Generic;
using EPE.Application.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPE.UI.Pages.Store
{
    public class ProductsModel : PageModel
    {
        public IEnumerable<GetProductsByCategoryName.ProductViewModel> Products { get; set; }

        public void OnGet([FromServices] GetProductsByCategoryName getProductsByCategoryName, string name)
        {
            Products = getProductsByCategoryName.Do(name);
        }
    }
}