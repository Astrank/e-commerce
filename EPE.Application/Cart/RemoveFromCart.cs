using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPE.Database;
using EPE.Domain.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace EPE.Application.Cart
{
    public class RemoveFromCart
    {
        private ISession _session;
        private ApplicationDbContext _ctx;

        public RemoveFromCart(ISession session, ApplicationDbContext ctx)
        {
            _session = session;
            _ctx = ctx;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }

        public async Task<bool> Do(Request request)
        {
            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString("cart");

            if (String.IsNullOrEmpty(stringObject))
            {
                return true;
            }

            cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);

            if (!cartList.Any(x => x.StockId == request.StockId))
            {
                return true;
            }

            var product = cartList.Find(x => x.StockId == request.StockId);

            if (product.Qty > request.Qty)
            {
                product.Qty -= request.Qty;
            }

            stringObject = JsonConvert.SerializeObject(cartList);

            _session.SetString("cart", stringObject);

            var stockOnHold = _ctx.StockOnHold
                .FirstOrDefault(x => x.StockId == request.StockId && x.SessionId == _session.Id);

            var stock = _ctx.Stock.FirstOrDefault(x => x.Id == request.StockId);
            
            if (stockOnHold.Qty > request.Qty)
            {
                stockOnHold.Qty -= request.Qty;
                stock.Qty += request.Qty;

            }

            await _ctx.SaveChangesAsync();

            return true;
        }
    }
}