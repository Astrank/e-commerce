using System.Collections.Generic;
using System.Linq;
using EPE.Domain.Infrastructure;

namespace EPE.Application.Cart
{
    [Service]
    public class GetCart
    {
        private ISessionManager _sessionManager;
        public GetCart(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public class Response
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public decimal RealValue { get; set; }
            public string Description { get; set; }
            public int StockId { get; set; }
            public int Qty { get; set; }
            public string Image { get; set; }
        }

        public IEnumerable<Response> Do()
        {
            return _sessionManager
                .GetCart()
                .Select(x => new Response
                {
                    Name = x.ProductName,
                    Value = x.Value.ValueToString(),
                    RealValue = x.Value,
                    Image = x.Image,
                    Description = x.Description,
                    StockId = x.StockId,
                    Qty = x.Qty
                });
        }
    }
}