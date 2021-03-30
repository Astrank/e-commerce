using System.Collections.Generic;
using EPE.Application.Categories;
using EPE.Application.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPE.UI.Pages.Store
{
    public class ProductsModel : PageModel
    {
        public IEnumerable<GetProductsByCategoryName.ProductViewModel> Products { get; set; }
        public GetCategory.Response Category { get; set; }

        public void OnGet(
            [FromServices] GetProductsByCategoryName getProductsByCategoryName,
            [FromServices] GetCategory getCategory,
            string name)
        {
            Products = getProductsByCategoryName.Do(name);
            Category = getCategory.Do(name);
        }
    }
}