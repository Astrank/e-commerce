using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPE.Database;
using Microsoft.EntityFrameworkCore;

namespace EPE.Application.Products
{
    public class GetProducts
    {
        private ApplicationDbContext _context;
        public GetProducts(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductViewModel>> Do() {
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
            var products = _context.Products
                .Include(x => x.Stock)
                .Select(x => new ProductViewModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    Value = $"$ {x.Value.ToString("N2")}",
                    Image = x.Image,

                    StockCount = x.Stock.Sum(y => y.Qty)
                }).ToList();

            return products;
        } 

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
            public string Image { get; set; }

            public int StockCount { get; set; }
        }
    }
}