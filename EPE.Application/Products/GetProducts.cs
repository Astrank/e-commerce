using System.Collections.Generic;
using System.Linq;
using EPE.Database;

namespace EPE.Application.Products
{
    public class GetProducts
    {
        private ApplicationDbContext _context;
        public GetProducts(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductViewModel> Do() => 
            _context.Products.ToList().Select(x => new ProductViewModel
            {
                Name = x.Name,
                Description = x.Description,
                Value = $"$ {x.Value.ToString("N2")}",
                Image = x.Image
            });

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
            public string Image { get; set; }
        }
    }
}