using EPE.Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EPE.Domain.Infrastructure;
using System.Collections.Generic;

namespace EPE.Database
{
    public class StockManager : IStockManager
    {
        private ApplicationDbContext _ctx;

        public StockManager(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public Task<int> CreateStock(Stock stock)
        {
            _ctx.Stock.Add(stock);

            return _ctx.SaveChangesAsync();
        }

        public List<TResult> GetStockById<TResult>(int productId, Func<Stock, TResult> selector)
        {
            return _ctx.Stock
                .Where(x => x.ProductId == productId)
                .Select(selector)
                .ToList();
        }

        public Stock GetStockWithProduct(int stockId)
        {
            return _ctx.Stock
                .Include(x => x.Product)
                .FirstOrDefault(x => x.Id == stockId);
        }

        public Task<int> UpdateStockRange(List<Stock> stockList)
        {
            _ctx.Stock.UpdateRange(stockList);

            return _ctx.SaveChangesAsync();
        }

        public Task<int> DeleteStock(int id)
        {
            var stock = _ctx.Stock.FirstOrDefault(x => x.Id == id);

            _ctx.Stock.Remove(stock);

            return _ctx.SaveChangesAsync();
        }

        public bool EnoughStock(int stockId, int qty)
        {
            return _ctx.Stock.FirstOrDefault(x => x.Id == stockId).Qty >= qty;
        }


        public Task PutStockOnHold(int stockId, int qty, string sessionId)
        {
            _ctx.Stock.FirstOrDefault(x => x.Id == stockId).Qty -= qty;

            var stockOnHold = _ctx.StockOnHold
                .Where(x => x.SessionId == sessionId)
                .ToList();

            if (stockOnHold.Any(x => x.StockId == stockId))
            {
                stockOnHold.Find(x => x.StockId == stockId).Qty += qty;
            }
            else
            {
                _ctx.StockOnHold.Add(new StockOnHold
                {
                    StockId = stockId,
                    SessionId = sessionId,
                    Qty = qty,
                    ExpiryDate = DateTime.Now.AddMinutes(20)
                });
            };

            foreach (var stock in stockOnHold)
            {                                                         //Not ideal way to handle
                stock.ExpiryDate = DateTime.Now.AddMinutes(20);       //Better add a filter as the person make requests
            }

            return _ctx.SaveChangesAsync();
        }

        public Task RemoveStockFromHold(string sessionId)
        {
            var stockList = _ctx.StockOnHold.ToList();
            var stockOnHold = stockList.Where(x => x.SessionId == sessionId).ToList();
            
            _ctx.StockOnHold.RemoveRange(stockOnHold);

            return _ctx.SaveChangesAsync();
        }

        public Task RemoveStockFromHold(int stockId, int qty, string sessionId)
        {
            var stockOnHold = _ctx.StockOnHold
                .FirstOrDefault(
                    x => x.StockId == stockId &&
                    x.SessionId == sessionId);

            var stock = _ctx.Stock.FirstOrDefault(x => x.Id == stockId);

            if (stockOnHold.Qty > qty)
            {   
                stockOnHold.Qty -= qty;
                stock.Qty += qty;
            }

            return _ctx.SaveChangesAsync();
        }

        public Task DeleteAllFromHold(int stockId, string sessionId)
        {
            var stockOnHold = _ctx.StockOnHold
                .FirstOrDefault(
                    x => x.StockId == stockId &&
                    x.SessionId == sessionId);

            var stock = _ctx.Stock.FirstOrDefault(x => x.Id == stockId);

            stock.Qty += stockOnHold.Qty;

            _ctx.StockOnHold.Remove(stockOnHold);

            return _ctx.SaveChangesAsync();
        }

        public Task RetrieveExpiredStockOnHold()
        {
            var sohList = _ctx.StockOnHold.ToList();
            var stockOnHold = sohList.Where(x => x.ExpiryDate < DateTime.Now).ToList();

            if (stockOnHold.Count > 0)
            {
                var strList = _ctx.Stock.ToList();
                var stockToReturn = strList.Where(x => stockOnHold.Any(y => y.StockId == x.Id)).ToList();

                foreach (var stock in stockToReturn)
                {
                    stock.Qty += stockOnHold.FirstOrDefault(x => x.StockId == stock.Id).Qty;
                }

                _ctx.StockOnHold.RemoveRange(stockOnHold);

                return _ctx.SaveChangesAsync();
            }

            return Task.CompletedTask;
        }
    }
}