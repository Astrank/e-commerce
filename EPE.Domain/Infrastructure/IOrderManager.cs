using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPE.Domain.Models;

namespace EPE.Domain.Infrastructure
{
    public interface IOrderManager
    {
        Task<int> CreateOrder(Order order);
        IEnumerable<TResult> GetOrdersByStatus<TResult>(OrderStatus status, Func<Order, TResult> selector);
        TResult GetOrderById<TResult>(int id, Func<Order, TResult> selector);
        TResult GetOrderByReference<TResult>(string reference, Func<Order, TResult> selector);
        bool OrderReferenceExists(string reference);
        Task<int> AdvanceOrderStatus(int id);
    }
}
