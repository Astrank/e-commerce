using System.Collections.Generic;
using System.Linq;
using EPE.Domain.Infrastructure;
using EPE.Domain.Models;

namespace EPE.Application.Cart
{
    [Service]
    public class GetOrder
    {
        private ISessionManager _sessionManager;
        public GetOrder(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public class Response
        {
            public IEnumerable<Product> Products { get; set; }
            public CustomerInformation CustomerInformation { get; set; }

            public int GetTotalCharge() => Products.Sum(x => x.Value * x.Qty);
        }

        public class Product
        {
            public int ProductId { get; set; }
            public int StockId { get; set; }
            public int Qty { get; set; }
            public int Value { get; set; }
        }

        public Response Do()
        {
            var products = _sessionManager
                .GetCart()
                .Select(x => new Product
                {
                    ProductId = x.ProductId,
                    StockId = x.StockId,
                    Value = (int)x.Value, // Stripe
                    Qty = x.Qty
                });

            var customerInfo = _sessionManager.GetCustomerInformation();

            return new Response
            {
                Products = products,
                CustomerInformation = new CustomerInformation
                {
                    FirstName = customerInfo.FirstName,
                    LastName = customerInfo.LastName,
                    Email = customerInfo.Email,
                    PhoneNumber = customerInfo.PhoneNumber,
                    Address1 = customerInfo.Address1,
                    Address2 = customerInfo.Address2,
                    City = customerInfo.City,   
                    PostCode = customerInfo.PostCode
                }
            };
        }
    }
}