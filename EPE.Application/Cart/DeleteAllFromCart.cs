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
    public class DeleteAllFromCart
    {
        private ISession _session;
        private ApplicationDbContext _ctx;

        public DeleteAllFromCart(ISession session, ApplicationDbContext ctx)
        {
            _session = session;
            _ctx = ctx;
        }

        public async Task<bool> Do(int stockId)
        {
            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString("cart");

            if (String.IsNullOrEmpty(stringObject))
            {
                return false;
            }

            cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);

            if (!cartList.Any(x => x.StockId == stockId))
            {
                return false;
            }

            var product = cartList.Find(x => x.StockId == stockId);

            cartList.Remove(product);

            stringObject = JsonConvert.SerializeObject(cartList);

            _session.SetString("cart", stringObject);


            var stockOnHold = _ctx.StockOnHold
                .FirstOrDefault(x => x.StockId == stockId && x.SessionId == _session.Id);

            var stock = _ctx.Stock.FirstOrDefault(x => x.Id == stockId);

            stock.Qty += stockOnHold.Qty;

            _ctx.StockOnHold.Remove(stockOnHold);

            await _ctx.SaveChangesAsync();

            return true;
        }
    }
}