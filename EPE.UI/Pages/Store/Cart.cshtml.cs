using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPE.Application.Cart;
using EPE.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace EPE.UI.Pages.Store
{
    public class CartModel : PageModel
    {
        private ApplicationDbContext _ctx;
        private IConfiguration _config;
        public CartModel(ApplicationDbContext ctx, IConfiguration config)
        {
            _ctx = ctx;
            _config = config;
        }

        public IEnumerable<GetCart.Response> Cart { get; set; }

        public IActionResult OnGet()
        {
            Cart = new GetCart(HttpContext.Session, _ctx).Do();
            
            return Page();
        }

        /*public IActionResult OnPost()
        {
            Cart = new GetCart(HttpContext.Session, _ctx).Do();

            double total = 1;

            var payPalAPI = new PayPalAPI(_config);
            string url = payPalAPI.getRedirectURLToPayPal(total, "USD");

            return Redirect(url);
        }*/
    }
}
