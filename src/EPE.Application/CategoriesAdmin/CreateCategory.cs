using EPE.Domain.Infrastructure;
using EPE.Domain.Models;
using System.Threading.Tasks;

namespace EPE.Application.CategoriesAdmin
{
    [Service]
    public class CreateCategory
    {
        private readonly ICategoryManager _categoryManager;

        public CreateCategory(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public class Request
        {
            public string Name { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public async Task<Response> Do(Request request)
        {
            var category = new Category
            {
                Name = request.Name
            };

            if(await _categoryManager.CreateCategory(category) <= 0)
            {
                throw new System.Exception("ERROR CREATING CATEGORY");
            };

            return new Response
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}