using EPE.Domain.Infrastructure;
using EPE.Domain.Models;
using System;
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
            return _categoryManager.GetSubcategories(Projection);
        }

        private static Func<Subcategory, Response> Projection = (sc) =>
            new Response
            {
                Id = sc.Id,
                Name = sc.Name,
                CategoryId = sc.CategoryId
            };
    }
}