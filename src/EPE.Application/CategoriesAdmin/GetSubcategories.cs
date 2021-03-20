using EPE.Domain.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace EPE.Application.CategoriesAdmin
{
    [Service]
    public class GetSubcategories
    {
        private readonly ICategoryManager _categoryManager;

        public GetSubcategories(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int CategoryId { get; set; }
        }

        public IEnumerable<Response> Do()
        {
            var subcategories = _categoryManager.GetSubcategories();

            return subcategories.Select(x => new Response
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId
            });
        }
    }
}