using System.Collections.Generic;
using System.Threading.Tasks;
using EPE.Domain.Infrastructure;
using EPE.Domain.Models;

namespace EPE.Application.ProductsAdmin
{
    [Service]
    public class CreateProduct
    {
        private readonly IProductManager _productManager;

        public CreateProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }
            
        public class Request
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public string PrimaryImage { get; set; }
            public List<string> Images { get; set; }
            public int CategoryId { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public string Image { get; set; }
            public int CategoryId { get; set; }
        }
        
        public async Task<Response> Do(Request request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Value = request.Value,
                PrimaryImage = request.PrimaryImage,
                CategoryId = request.CategoryId
            };
            
            if(await _productManager.CreateProduct(product) <= 0)
            {
                throw new System.Exception("Failed to create product"); //TODO: custom exceptions
            };

            /* IMAGES */

            if (request.Images.Count > 0)
            {
                var productImages = new List<ProductImage>();

                foreach (var image in request.Images)
                {
                    var productImage = new ProductImage
                    {
                        Path = image,
                        ProductId = product.Id
                    };

                    productImages.Add(productImage);
                };

                if(await _productManager.SaveProductImages(productImages) <= 0)
                {
                    throw new System.Exception("Failed saving images"); //TODO: custom exceptions
                };
            }

            /* --------------- */

            return new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value,
                CategoryId = product.CategoryId
            };
        }
    }
}