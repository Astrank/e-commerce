using System.Collections.Generic;
using System.Linq;
using EPE.Database;
using EPE.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EPE.Application.ProductsAdmin
{
    public class GetProducts
    {
        private ApplicationDbContext _context;
        public GetProducts(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductViewModel> Do(){ 
            var asd = _context.Products.Include(x => x.Stock).Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Value = x.Value,
                Description = x.Description,
                
                Stock = x.Stock,
                StockCount = x.Stock.Sum(y => y.Qty)
            });

            return asd;
        }

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Value { get; set; }
            public string Description { get; set; }

            public ICollection<Stock> Stock { get; set; }
            public int StockCount { get; set; }
        }
    }
}   