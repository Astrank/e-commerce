using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPE.Domain.Infrastructure;
using EPE.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EPE.Database
{
    public class ProductManager : IProductManager
    {
        private readonly ApplicationDbContext _ctx;

        public ProductManager(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public Task<int> CreateProduct(Product product)
        {
            _ctx.Products.Add(product);

            return _ctx.SaveChangesAsync();
        }

        public IEnumerable<TResult> GetProductsWithStock<TResult>(Func<Product, TResult> selector)
        {
            return _ctx.Products
                .Include(x => x.Stock)
                .Select(selector)
                .ToList();
        }

        public IEnumerable<TResult> GetProductsByCategoryName<TResult>(string name, Func<Product, TResult> selector)
        {
            var products = _ctx.Products
                .FromSqlRaw("spGetProductsByCategoryName {0}", name)
                .Select(selector);

            return products;
        }

        public TResult GetProductById<TResult>(int id, Func<Product, TResult> selector)
        {
            return _ctx.Products
                .Include(x => x.Images)
                .Where(x => x.Id == id)
                .Select(selector)
                .FirstOrDefault();
        }

        public TResult GetProductByName<TResult>(string name, Func<Product, TResult> selector)
        {
            return _ctx.Products
                .Include(x => x.Images)
                .Include(x => x.Stock)
                .Where(x => x.Name == name)
                .Select(selector)
                .FirstOrDefault();
        }

        public Task<int> SaveProductImages(List<ProductImage> productImages)
        {
            _ctx.ProductImages.AddRange(productImages);

            return _ctx.SaveChangesAsync();
        }

        public Task<int> UpdateProduct(Product product)
        {
            _ctx.Products.Update(product);

            return _ctx.SaveChangesAsync();
        }

        public async Task<List<string>> DeleteProduct(int id)
        {
            var product = _ctx.Products
                .Include(x => x.Images)
                .FirstOrDefault(x => x.Id == id);

            _ctx.Products.Remove(product);

            if (await _ctx.SaveChangesAsync() > 0)
            {
                var images = new List<string>
                {
                    product.PrimaryImage
                };

                foreach (var image in product.Images)
                {
                    images.Add(image.Path);   
                }

                return images;
            }

            throw new System.Exception("Error deleting a product");
        }
    }
}