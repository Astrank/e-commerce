using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPE.Database;
using EPE.Database.FileManager;
using EPE.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace EPE.Application.ProductsAdmin
{
    public class CreateProduct
    {
        private ApplicationDbContext _context;
        private IFileManager _fileManager;

        public CreateProduct(ApplicationDbContext context, IFileManager fileManager)
        {
            _context = context;
            _fileManager = fileManager;
        }
        
        public string rootPath = "ProductsPath:Images";

        public async Task<Response> Do(Request request)
        {
            var imagePath = await _fileManager.SaveImage(rootPath, request.Image);

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Value = request.Value,
                Image = imagePath,
            };
            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value,
                Image = product.Image,
            };
        }
            
        public class Request
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public IFormFile Image { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public string Image { get; set; }
        }
    }
}