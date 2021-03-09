using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPE.Domain.Infrastructure;
using EPE.Domain.Models;

namespace EPE.Application.Products
{
    [Service]
    public class GetProduct
    {
        private readonly IProductManager _productManager;
        private readonly IStockManager _stockManager;

        public GetProduct(IProductManager productManager, IStockManager stockManager)
        {
            _productManager = productManager;
            _stockManager = stockManager;
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
            public int Qty { get; set; }
        }
        public async Task<ProductViewModel> Do(string name)
        {
            await _stockManager.RetrieveExpiredStockOnHold();

            return _productManager.GetProductByName(name, Projection);
        }
        
        private static Func<Product, ProductViewModel> Projection = (product) =>
            new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Value = product.Value.ValueToString(),

                Stock = product.Stock.Select(y => new StockViewModel
                {
                    Id = y.Id,
                    Description = y.Description,
                    Qty = y.Qty
                })
            };
    }
}