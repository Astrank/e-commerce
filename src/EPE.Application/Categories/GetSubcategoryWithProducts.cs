using EPE.Domain.Infrastructure;
using EPE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EPE.Application.Categories
{
    [Service]
    public class GetSubcategoryWithProducts
    {
        private readonly ICategoryManager _categoryManager;

        public GetSubcategoryWithProducts(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public class Response
        {
            public string Name { get; set; }
            public string Category { get; set; }
            public IEnumerable<ProductViewModel> Products { get; set; }
        }

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public string PrimaryImage { get; set; }
            public int StockCount { get; set; }
        }

        public Response Do(string name)
        {
            return _categoryManager.GetSubcategoryWithProducts(name, Projection);
        }

        private static Func<Subcategory, Response> Projection = (sc) =>
            new Response
            {
                Name = sc.Name,
                Category = sc.Category.Name,
                Products = sc.Products.Select(y => new ProductViewModel
                {
                    Name = y.Name,
                    Description = y.Description,
                    Value = y.Value,
                    PrimaryImage = y.PrimaryImage,
                    StockCount = y.Stock.Sum(z => z.Qty)
                })
            };
    }
}