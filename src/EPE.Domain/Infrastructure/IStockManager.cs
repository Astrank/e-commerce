using EPE.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace EPE.Domain.Infrastructure
{
    public interface IStockManager
    {
        Task<int> CreateStock(Stock stock);
        List<TResult> GetStockById<TResult>(int id, Func<Stock, TResult> selector);
        Stock GetStockWithProduct(int stockId);
        Task<int> UpdateStockRange(List<Stock> stockList);
        Task<int> DeleteStock(int id);
        bool EnoughStock(int stockId, int qty);
        
        Task PutStockOnHold(int stockId, int qty, string sessionId);
        Task RemoveStockFromHold(string sessionId);
        Task RemoveStockFromHold(int stockId, int qty, string sessionId);
        Task DeleteAllFromHold(int stockId, string sessionId);
        Task RetrieveExpiredStockOnHold();
    }
}