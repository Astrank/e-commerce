using System;
using System.Collections.Generic;
using System.Linq;
using EPE.Database;
using EPE.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EPE.Application.Cart
{
    public class GetOrder
    {
        private ApplicationDbContext _ctx;
        private ISession _session;
        public GetOrder(ISession session, ApplicationDbContext ctx)
        {
            _session = session;
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
            var cart = _session.GetString("cart");

            var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(cart);

            var productsList = _ctx.Stock
                .Include(x => x.Product).ToList()
                .Where(x => cartList.Any(y => y.StockId == x.Id))
                .Select(x => new Product
                {
                    ProductId = x.ProductId,
                    StockId = x.Id,
                    Value = (int) x.Product.Value, // Stripe
                    Qty = cartList.FirstOrDefault(y => y.StockId == x.Id).Qty
                }).ToList();

            var customerInfoString = _session.GetString("customer-info");

            var customerInfo = JsonConvert.DeserializeObject<CustomerInformation>(customerInfoString);

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