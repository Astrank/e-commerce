using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPE.Application.Cart;
using EPE.Application.Products;
using EPE.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPE.UI.Pages.Store
{
    public class ProductModel : PageModel
    {
        private ApplicationDbContext _context;
        public ProductModel (ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AddToCart.Request CartViewModel { get; set; }

        public GetProduct.ProductViewModel Product { get; set; }

        public async Task<IActionResult> OnGet(string name)
        {
            Product = await new GetProduct(_context).Do(name.Replace("-", " "));

            if(Product == null)
                return RedirectToPage("Index");
            else
                return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var stockAdded = await new AddToCart(HttpContext.Session, _context).Do(CartViewModel);

            if (stockAdded)
                return RedirectToPage("Cart");
            else
                //TODO: add a warning
                return Page();
        }
    }
}