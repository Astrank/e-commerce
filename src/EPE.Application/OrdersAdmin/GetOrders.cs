using System;
using System.Collections.Generic;
using System.Linq;
using EPE.Domain;
using EPE.Domain.Infrastructure;
using EPE.Domain.Models;

namespace EPE.Application.OrdersAdmin
{
    [Service]
    public class GetOrders
    {
        private readonly IOrderManager _orderManager;

        public GetOrders(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public class Response 
        {
            public int Id { get; set; }
            public string OrderRef { get; set; }
            public string Email { get; set; }
        }

        public IEnumerable<Response> Do(int status) =>
            _orderManager.GetOrdersByStatus((OrderStatus) status, Projection);

        private static Func<Order, Response> Projection = (order) =>
            new Response
            {
                Id = order.Id,
                OrderRef = order.OrderRef,
                Email = order.Email
            };
    }
}