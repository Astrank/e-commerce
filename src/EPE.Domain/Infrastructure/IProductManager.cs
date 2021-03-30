using EPE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPE.Domain.Infrastructure
{
    public interface IProductManager
    {
        Task<int> CreateProduct(Product product);
        IEnumerable<TResult> GetProductsWithStock<TResult>(Func<Product, TResult> selector);
        IEnumerable<TResult> GetProductsByCategoryName<TResult>(string name, Func<Product, TResult> selector);
        TResult GetProductById<TResult>(int id, Func<Product, TResult> selector);
        TResult GetProductByName<TResult>(string name, Func<Product, TResult> selector);
        Task<int> SaveProductImages(List<ProductImage> productImages);
        Task<int> UpdateProduct(Product product);
        Task<List<string>> DeleteProduct(int id);
    }
}
