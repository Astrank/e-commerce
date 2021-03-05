using System.Collections.Generic;
using System.Linq;
using EPE.Application.Infrastructure;
using EPE.Database;
using EPE.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EPE.Application.Cart
{
    public class GetOrder
    {
        private ApplicationDbContext _ctx;
        private ISessionManager _sessionManager;
        public GetOrder(ISessionManager sessionManager, ApplicationDbContext ctx)
        {
            _sessionManager = sessionManager;
            _ctx = ctx;
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
            var cart = _sessionManager.GetCart();

            var productsList = _ctx.Stock
                .Include(x => x.Product).ToList()
                .Where(x => cart.Any(y => y.StockId == x.Id))
                .Select(x => new Product
                {
                    ProductId = x.ProductId,
                    StockId = x.Id,
                    Value = (int) x.Product.Value, // Stripe
                    Qty = cart.FirstOrDefault(y => y.StockId == x.Id).Qty
                }).ToList();

            var customerInfo = _sessionManager.GetCustomerInformation();

            return new Response
            {
                Products = productsList,
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