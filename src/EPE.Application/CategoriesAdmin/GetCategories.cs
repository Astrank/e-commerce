using EPE.Domain.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace EPE.Application.CategoriesAdmin
{
    [Service]
    public class GetCategories
    {
        private readonly ICategoryManager _categoryManager;

        public GetCategories(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public IEnumerable<Response> Do()
        {
            var categories = _categoryManager.GetCategories();

            return categories.Select(x => new Response
            {
                Id = x.Id,
                Name = x.Name
            });
        }
    }
}