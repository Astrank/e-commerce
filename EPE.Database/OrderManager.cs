using System.Threading.Tasks;
using EPE.Domain.Infrastructure;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using EPE.Domain.Models;
using EPE.Domain;
using System.Collections.Generic;

namespace EPE.Database
{
    public class OrderManager : IOrderManager
    {
        private readonly ApplicationDbContext _ctx;

        public OrderManager(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public Task<int> CreateOrder(Order order)
        {
            _ctx.Orders.Add(order);

            return _ctx.SaveChangesAsync();
        }

        private TResult GetOrder<TResult>(
            Func<Order, bool> condition, 
            Func<Order, TResult> selector)
        {
            return _ctx.Orders
                .Where(x => condition(x))
                .Include(x => x.OrderStocks)
                    .ThenInclude(x => x.Stock)
                        .ThenInclude(x => x.Product)
                .Select(selector)
                .FirstOrDefault();
        }

        public IEnumerable<TResult> GetOrdersByStatus<TResult>(OrderStatus status, Func<Order, TResult> selector)
        {
            return _ctx.Orders
                .Where(x => x.Status == status)
                .Select(selector)
                .ToList();
        }

        public TResult GetOrderById<TResult>(int id, Func<Order, TResult> selector)
        {
            return _ctx.Orders
                .Where(order => order.Id == id)
                .Include(x => x.OrderStocks)
                    .ThenInclude(x => x.Stock)
                        .ThenInclude(x => x.Product)
                .Select(selector)
                .FirstOrDefault();

            //return GetOrder(order => order.Id == id, selector);
        }

        public TResult GetOrderByReference<TResult>(string reference, Func<Order, TResult> selector)
        {
            return GetOrder(x => x.OrderRef == reference, selector);
        }

        public bool OrderReferenceExists(string reference)
        {
            return _ctx.Orders.Any(x => x.OrderRef == reference);
        }

        public Task<int> AdvanceOrderStatus(int id)
        {
            _ctx.Orders.FirstOrDefault(x => x.Id == id).Status++;

            return _ctx.SaveChangesAsync(); 
        }
    }
}