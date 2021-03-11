using System.Linq;
using System.Threading.Tasks;
using EPE.Application.Cart;
using EPE.Application.Orders;
using EPE.Domain.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GetOrderCart = EPE.Application.Cart.GetOrder;

namespace EPE.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        public IActionResult OnGet([FromServices] GetCustomerInformation getCustomerInformation)
        {
            var info = getCustomerInformation.Do();

            if (info == null)
            {
                return RedirectToPage("/Checkout/CustomerInformation");
            }

            return Page();
        }

        public async Task<IActionResult> OnPost(
            [FromServices] GetOrderCart getOrder, 
            [FromServices] CreateOrder createOrder,
            [FromServices] ISessionManager sessionManager)
        {
            // Stripe

            var cartOrder = getOrder.Do();

            var sessionId = HttpContext.Session.Id;

            await createOrder.Do(new CreateOrder.Request
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

            sessionManager.ClearCart();

            return RedirectToPage("/Index");  
        }
    }
}
