using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPE.Database;
using EPE.Domain.Models;

namespace EPE.Application.StockAdmin
{
    public class UpdateStock
    {
        private ApplicationDbContext _ctx;
        public UpdateStock(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Response> Do(Request request)
        {
            var stocks = new List<Stock>();

            foreach (var stock in request.Stock)
            {
                stocks.Add(new Stock
                {
                    Id = stock.Id,
                    ProductId = stock.ProductId,
                    Qty = stock.Qty,
                    Description = stock.Description
                });
            };

            _ctx.Stock.UpdateRange(stocks);

            await _ctx.SaveChangesAsync();
            
            return new Response
            {
                Stock = request.Stock
            };
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }

        public class Request
        {
            public IEnumerable<StockViewModel> Stock { get; set; }
        }

        public class Response
        {
            public IEnumerable<StockViewModel> Stock { get; set; }

        }
    }
}