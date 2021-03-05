using System;
using System.Collections.Generic;
using System.Linq;
using EPE.Application.Infrastructure;
using EPE.Database;
using EPE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EPE.Application.Cart
{
    public class GetCart
    {
        private ApplicationDbContext _ctx;
        private ISessionManager _sessionManager;
        public GetCart(ISessionManager sessionManager, ApplicationDbContext ctx)
        {
            _sessionManager = sessionManager;
            _ctx = ctx;
        }

        public class Response
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public string Description { get; set; }
            public int StockId { get; set; }
            public int Qty { get; set; }
            public string Image { get; set; }
        }

        public IEnumerable<Response> Do()
        {
            var cartList = _sessionManager.GetCart();

            if (cartList == null)
            {
                return new List<Response>(); 
            }

            var response = _ctx.Stock
                .Include(x => x.Product).AsEnumerable()
                .Where(x => cartList.Any(y => y.StockId == x.Id))
                .Select(x => new Response
                {
                    Name = x.Product.Name,
                    Value = $"$ {x.Product.Value.ToString("N2")}",
                    Image = x.Product.Image,
                    Description = x.Description,
                    StockId = x.Id,
                    Qty = cartList.FirstOrDefault(y => y.StockId == x.Id).Qty
                })
                .ToList();

            return response;
        }
    }
}