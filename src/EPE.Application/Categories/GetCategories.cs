using EPE.Domain.Infrastructure;
using EPE.Domain.Models;
using System;
using System.Collections.Generic;

namespace EPE.Application.Categories
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
            public string Name { get; set; }
        }

        public IEnumerable<Response> Do()
        {
            return _categoryManager.GetCategories(Projection);
        }

        private static Func<Category, Response> Projection = (category) =>
            new Response
            {
                Name = category.Name
            };
    }
}
