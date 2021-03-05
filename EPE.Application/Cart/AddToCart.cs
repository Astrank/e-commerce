using EPE.Application.Infrastructure;
using EPE.Database;
using EPE.Domain.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPE.Application.Cart
{
    public class AddToCart
    {
        private ISessionManager _sessionManager;
        private ApplicationDbContext _ctx;

        public AddToCart(ISessionManager sessionManager, ApplicationDbContext ctx)
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
            var stockOnHold = _ctx.StockOnHold.Where(x => x.SessionId == _sessionManager.GetId()).ToList();
            var stockToHold = _ctx.Stock.Where(x => x.Id == request.StockId).FirstOrDefault();

            if (stockToHold.Qty < request.Qty)
            {
                //TODO: return not enough stock
                return false;
            }

            if (stockOnHold.Any(x => x.StockId == request.StockId))
            {
                stockOnHold.Find(x => x.StockId == request.StockId).Qty += request.Qty;
            }
            else
            {
                _ctx.StockOnHold.Add(new StockOnHold
                {
                    StockId = stockToHold.Id,
                    SessionId = _sessionManager.GetId(),
                    Qty = request.Qty,
                    ExpiryDate = DateTime.Now.AddMinutes(20)
                });
            };

            stockToHold.Qty -= request.Qty;

            foreach (var stock in stockOnHold)
            {                                                         //Not ideal way to handle
                stock.ExpiryDate = DateTime.Now.AddMinutes(20);       //Better add a filter as the person make requests
            }

            await _ctx.SaveChangesAsync();

            _sessionManager.AddProduct(request.StockId, request.Qty);

            return true;
        }
    }
}