using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPE.Application.Infrastructure;
using EPE.Database;
using EPE.Domain.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace EPE.Application.Cart
{
    public class DeleteAllFromCart
    {
        private ISessionManager _sessionManager;
        private ApplicationDbContext _ctx;

        public DeleteAllFromCart(ISessionManager sessionManager, ApplicationDbContext ctx)
        {
            _sessionManager = sessionManager;
            _ctx = ctx;
        }

        public async Task<bool> Do(int stockId)
        {
            _sessionManager.DeleteAllFromCart(stockId);

            var stockOnHold = _ctx.StockOnHold
                .FirstOrDefault(x => x.StockId == stockId && x.SessionId == _sessionManager.GetId());

            var stock = _ctx.Stock.FirstOrDefault(x => x.Id == stockId);

            stock.Qty += stockOnHold.Qty;

            _ctx.StockOnHold.Remove(stockOnHold);

            await _ctx.SaveChangesAsync();

            return true;
        }
    }
}