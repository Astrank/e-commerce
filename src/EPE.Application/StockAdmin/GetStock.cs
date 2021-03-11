using System;
using System.Collections.Generic;
using EPE.Domain.Infrastructure;
using EPE.Domain.Models;

namespace EPE.Application.StockAdmin
{
    [Service]
    public class GetStock
    {
        private readonly IStockManager _stockManager;

        public GetStock(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }

        public List<StockViewModel> Do(int id)
        {
            return _stockManager.GetStockById(id, Projection);
        }

        private static Func<Stock, StockViewModel> Projection = (stock) =>
            new StockViewModel
            {
                Id = stock.Id,
                Description = stock.Description,
                Qty = stock.Qty,
                ProductId = stock.ProductId
            };

        /*private static Func<Product, ProductViewModel> Projection = (product) =>
            new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Stock = product.Stock.Select(y => new StockViewModel
                {
                    Id = y.Id,
                    Description = y.Description,
                    Qty = y.Qty
                })
            };*/

        public class StockViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
            public int ProductId { get; set; }
        }

        public class ProductViewModel 
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
        }
    }
}