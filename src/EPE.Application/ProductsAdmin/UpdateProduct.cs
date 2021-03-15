using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPE.Domain.Infrastructure;
using EPE.Domain.Models;

namespace EPE.Application.ProductsAdmin
{
    [Service]
    public class UpdateProduct
    {
        private readonly IProductManager _productManager;

        public UpdateProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public class Request
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public string PrimaryImage { get; set; }
            public List<string> Images { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public string PrimaryImage { get; set; }
            public IEnumerable<string> Images { get; set; }
        }

        public async Task<Response> Do(Request request)
        {
            var product = _productManager.GetProductById(request.Id, x => x);

            product.Name = request.Name;
            product.Description = request.Description;
            product.Value = request.Value;
            product.PrimaryImage = request.PrimaryImage;

            List<ProductImage> images = new List<ProductImage>();

            foreach (var image in request.Images)
            {
                images.Add(new ProductImage
                {
                    ProductId = product.Id,
                    Path = image,
                });
            };

            product.Images = images;

            await _productManager.UpdateProduct(product);

            return new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value,
                PrimaryImage = product.PrimaryImage,
                Images = product.Images.Select(x => x.Path)
            };
        }
    }
}