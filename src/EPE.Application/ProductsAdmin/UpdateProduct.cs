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
            public string Image { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public string Image { get; set; }
        }

        public async Task<Response> Do(Request request)
        {
            var product = _productManager.GetProductById(request.Id, x => x);

            product.Name = request.Name;
            product.Description = request.Description;
            product.Value = request.Value;
            product.Image = request.Image;

            await _productManager.UpdateProduct(product);

            return new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value,
                Image = product.Image
            };
        }
    }
}