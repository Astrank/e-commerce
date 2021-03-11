using System.Collections.Generic;
using System.Threading.Tasks;
using EPE.Application.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPE.UI.Pages.Store
{
    public class IndexModel : PageModel
    {
        /*[BindProperty]
        public CreateProduct.Request Request { get; set; }*/
        public IEnumerable<GetProducts.ProductViewModel> Products { get; set; }

        public async Task OnGet([FromServices] GetProducts getProducts)
        {
            Products = await getProducts.Do();
        }

        /*public async Task<IActionResult> OnPost()
        {
            await new CreateProduct(_context).Do(Request);
            return RedirectToPage("Index");
        }*/
    }
}
