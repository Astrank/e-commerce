using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPE.Database;
using EPE.Database.FileManager;
using EPE.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace EPE.Application.ProductsAdmin
{
    public class UpdateProduct
    {
        private ApplicationDbContext _context;
        private IFileManager _fileManager;

        public UpdateProduct(ApplicationDbContext context, IFileManager fileManager)
        {
            _context = context;
            _fileManager = fileManager;
        }

        public string rootPath = "ProductsPath:Images";

        public async Task<Response> Do(Request request)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == request.Id);

            product.Name = request.Name;
            product.Description = request.Description;
            product.Value = request.Value;

            if (request.Image != null)
            {
                if (product.Image != null)
                {
                    _fileManager.DeleteImage(rootPath , product.Image);
                }

                var imgPath = await _fileManager.SaveImage(rootPath , request.Image);
                product.Image = imgPath.ToString();
            }

            await _context.SaveChangesAsync();
            return new Response{
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value,
                Image = product.Image
            };
        }

        public class Request
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public IFormFile Image { get; set; }

            public IList<Stock> Stock { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public string Image { get; set; }

            public IList<Stock> Stock { get; set; }
        }
    }
}