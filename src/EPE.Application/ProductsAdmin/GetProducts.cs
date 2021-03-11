using System;
using System.Collections.Generic;
using System.Linq;
using EPE.Domain.Infrastructure;
using EPE.Domain.Models;

namespace EPE.Application.ProductsAdmin
{
    [Service]
    public class GetProducts
    {
        private readonly IProductManager _productManager;

        public GetProducts(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Value { get; set; }
            public int StockCount { get; set; }
        }

        public IEnumerable<ProductViewModel> Do(){
            return _productManager.GetProductsWithStock(Projection);
        }

        private static Func<Product, ProductViewModel> Projection = (product) =>
            new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Value = product.Value,
                StockCount = product.Stock.Sum(y => y.Qty)
            };
    }
}   