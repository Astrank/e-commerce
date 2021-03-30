using System;
using System.Collections.Generic;
using EPE.Domain.Infrastructure;
using EPE.Domain.Models;

namespace EPE.Application.Products
{
    [Service]
    public class GetProductsByCategoryName
    {
        private readonly IProductManager _productManager;

        public GetProductsByCategoryName(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public string PrimaryImage { get; set; }
        }

        public IEnumerable<ProductViewModel> Do(string name)
        {
            return _productManager.GetProductsByCategoryName(name, Projection);
        }

        private static Func<Product, ProductViewModel> Projection = (product) =>
            new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Value = product.Value,
                PrimaryImage = product.PrimaryImage
            };
    }
}