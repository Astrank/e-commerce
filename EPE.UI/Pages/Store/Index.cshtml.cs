using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPE.Application.Products;
using EPE.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPE.UI.Pages.Store
{
    public class IndexModel : PageModel
    {
        private ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        /*[BindProperty]
        public CreateProduct.Request Request { get; set; }*/
        public IEnumerable<GetProducts.ProductViewModel> Products { get; set; }

        public void OnGet()
        {
            Products = new GetProducts(_context).Do();
        }

        /*public async Task<IActionResult> OnPost()
        {
            await new CreateProduct(_context).Do(Request);
            return RedirectToPage("Index");
        }*/
    }
}
