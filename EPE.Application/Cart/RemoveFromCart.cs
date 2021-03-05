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
    public class RemoveFromCart
    {
        private ISessionManager _sessionManager;
        private ApplicationDbContext _ctx;

        public RemoveFromCart(ISessionManager sessionManager, ApplicationDbContext ctx)
        {
            _sessionManager = sessionManager;
            _ctx = ctx;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }

        public async Task<bool> Do(Request request)
        {
            _sessionManager.RemoveProduct(request.StockId, request.Qty);

            var stockOnHold = _ctx.StockOnHold
                .FirstOrDefault(x => x.StockId == request.StockId && x.SessionId == _sessionManager.GetId());

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