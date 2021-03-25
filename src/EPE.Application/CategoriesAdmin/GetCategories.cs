using EPE.Domain.Infrastructure;
using EPE.Domain.Models;
using System;
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
            return _categoryManager.GetCategories(Projection);
        }

        private static Func<Category, Response> Projection = (categories) =>
            new Response
            {
                Id = categories.Id,
                Name = categories.Name
            };
    }
}