using EPE.Application.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPE.UI.Pages.Store
{
    public class OrderModel : PageModel
    {
        public GetOrder.Response Order { get; set; }

        public void OnGet([FromServices] GetOrder getOrder, string reference)
        {
            Order = getOrder.Do(reference);
        }
    }
}
