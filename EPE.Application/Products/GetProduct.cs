using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPE.Database;
using Microsoft.EntityFrameworkCore;

namespace EPE.Application.Products
{
    public class GetProduct
    {
        private ApplicationDbContext _context;
        
        public GetProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductViewModel> Do(string name)
        {
            var sohList = _context.StockOnHold.ToList();
            var stockOnHold = sohList.Where(x => x.ExpiryDate < DateTime.Now).ToList();

            if (stockOnHold.Count > 0)
            {
                var strList = _context.Stock.ToList();
                var stockToReturn = strList.Where(x => stockOnHold.Any(y => y.StockId == x.Id)).ToList();

                foreach (var stock in stockToReturn)
                {
                    stock.Qty += stockOnHold.FirstOrDefault(x => x.StockId == stock.Id).Qty;
                }

                _context.StockOnHold.RemoveRange(stockOnHold);

                await _context.SaveChangesAsync();
            }

            return _context.Products
                .Include(x => x.Stock)
                .Where(x => x.Name == name)
                .Select(x => new ProductViewModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    Value = $"$ {x.Value.ToString("N2")}",

                    Stock = x.Stock.Select(y => new StockViewModel
                    {
                        Id = y.Id,
                        Description = y.Description,
                        InStock = y.Qty > 0
                    })
                })
                .FirstOrDefault();
        }
            

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
        }

        public class StockViewModel 
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public bool InStock { get; set; }
        }
    }
}