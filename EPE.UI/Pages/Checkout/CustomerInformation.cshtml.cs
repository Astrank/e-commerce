using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPE.Application.Cart;
using EPE.Database;
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

        public IActionResult OnGet()
        {
            var info = new GetCustomerInformation(HttpContext.Session).Do();

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

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            new AddCustomerInformation(HttpContext.Session).Do(CustomerInfo);

            return RedirectToPage("/Checkout/Payment");
        }
    }
}
