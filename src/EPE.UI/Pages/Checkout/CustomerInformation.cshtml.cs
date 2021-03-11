using EPE.Application.Cart;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;

namespace EPE.UI.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {
        private IHostEnvironment _env;

        public CustomerInformationModel(IHostEnvironment env)
        {
            _env = env;
        }

        [BindProperty]
        public AddCustomerInformation.Request CustomerInfo { get; set; } 

        public IActionResult OnGet([FromServices] GetCustomerInformation getCustomerInformation)
        {
            var info = getCustomerInformation.Do();

            if (info == null)
            {
                if(_env.IsDevelopment())
                {
                    CustomerInfo = new AddCustomerInformation.Request
                    {
                        FirstName = "FirstName",
                        LastName = "LastName",
                        Email = "Email@asd.com",
                        PhoneNumber = "123",
                        Address1 = "Address1",
                        Address2 = "Address2",
                        City = "City",
                        PostCode = "PostCode",
                    };
                }
                return Page();
            }
            else
            {
                return RedirectToPage("/Checkout/Payment");
            }
        }

        public IActionResult OnPost([FromServices] AddCustomerInformation addCustomerInformation)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            addCustomerInformation.Do(CustomerInfo);

            return RedirectToPage("/Checkout/Payment");
        }
    }
}
