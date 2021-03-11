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

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Value = request.Value,
                Image = request.Image,
            };
            
            if(await _productManager.CreateProduct(product) <= 0)
            {
                throw new System.Exception("Failed to create product"); //TODO: custom exceptions
            };

            return new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value,
                Image = product.Image,
            };
        }
    }
}