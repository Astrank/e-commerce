using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPE.Domain.Infrastructure;
using EPE.Domain.Models;

namespace EPE.Application.Products
{
    [Service]
    public class GetProducts
    {
        private readonly IProductManager _productManager;
        private readonly IStockManager _stockManager;

        public GetProducts(IProductManager productManager, IStockManager stockManager)
        {
            _productManager = productManager;
            _stockManager = stockManager;
        }

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
            public string Image { get; set; }

            public int StockCount { get; set; }
        }

        public async Task<IEnumerable<ProductViewModel>> Do() 
        {
            await _stockManager.RetrieveExpiredStockOnHold();

            return _productManager.GetProductsWithStock(Projection);
        } 

        private static Func<Product, ProductViewModel> Projection = (product) =>
            new ProductViewModel
            {
                Name = product.Name,
                    Description = product.Description,
                    Value = product.Value.ValueToString(),
                    Image = product.PrimaryImage,

                    StockCount = product.Stock.Sum(y => y.Qty)
            };
    }
}