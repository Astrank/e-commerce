using EPE.Domain.Infrastructure;
using EPE.Domain.Models;
using System.Threading.Tasks;

namespace EPE.Application.CategoriesAdmin
{
    [Service]
    public class CreateSubcategory
    {
        private readonly ICategoryManager _categoryManager;

        public CreateSubcategory(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public class Request
        {
            public string Name { get; set; }
            public int CategoryId { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int CategoryId { get; set; }
        }

        public async Task<Response> Do(Request request)
        {
            var category = new Subcategory
            {
                Name = request.Name,
                CategoryId = request.CategoryId
            };

            if (await _categoryManager.CreateSubcategory(category) <= 0)
            {
                throw new System.Exception("ERROR CREATING SUBCATEGORY");
            };

            return new Response
            {
                Id = category.Id,
                Name = category.Name,
                CategoryId = category.CategoryId
            };
        }
    }
}