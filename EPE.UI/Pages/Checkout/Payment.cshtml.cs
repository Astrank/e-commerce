using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPE.Application.Cart;
using EPE.Application.Orders;
using EPE.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPE.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        private ApplicationDbContext _ctx;

        public PaymentModel(ApplicationDbContext ctx)
        {
            _ctx = ctx;    
        }

        public IActionResult OnGet()
        {
            var info = new GetCustomerInformation(HttpContext.Session).Do();

            if (info == null)
            {
                return RedirectToPage("/Checkout/CustomerInformation");
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            // Stripe

            var cartOrder = new EPE.Application.Cart.GetOrder(HttpContext.Session, _ctx).Do();

            var sessionId = HttpContext.Session.Id;

            await new CreateOrder(_ctx).Do(new CreateOrder.Request
            {
                SessionId = sessionId,

                FirstName = cartOrder.CustomerInformation.FirstName,
                LastName = cartOrder.CustomerInformation.LastName,
                Email = cartOrder.CustomerInformation.Email,
                PhoneNumber = cartOrder.CustomerInformation.PhoneNumber,
                Address1 = cartOrder.CustomerInformation.Address1,
                Address2 = cartOrder.CustomerInformation.Address2,
                City = cartOrder.CustomerInformation.City,   
                PostCode = cartOrder.CustomerInformation.PostCode,

                Stocks = cartOrder.Products.Select(x => new CreateOrder.Stock
                {
                    StockId = x.StockId,
                    Qty = x.Qty
                }).ToList()
            });

            return RedirectToPage("/Index");  
        }
    }
}
