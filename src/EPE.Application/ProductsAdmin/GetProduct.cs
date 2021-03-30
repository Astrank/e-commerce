using System;
using System.Collections.Generic;
using System.Linq;
using EPE.Domain.Infrastructure;
using EPE.Domain.Models;

namespace EPE.Application.ProductsAdmin
{
    [Service]
    public class GetProduct
    {
        private readonly IProductManager _productManager;

        public GetProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public string PrimaryImage { get; set; }
            public IEnumerable<string> Images { get; set; }
        }

        public ProductViewModel Do(int id) =>
            _productManager.GetProductById(id, Projection);

        private static Func<Product, ProductViewModel> Projection = (product) =>
            new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value,
                PrimaryImage = product.PrimaryImage,
                Images = product.Images.Select(x => x.Path),
            };
    }
}