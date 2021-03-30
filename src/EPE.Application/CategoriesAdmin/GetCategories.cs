using EPE.Domain.Infrastructure;
using EPE.Domain.Models;
using System;
using System.Collections.Generic;

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
            public CategoryViewModel Parent { get; set; }
            public IEnumerable<Product> Products { get; set; }
        }

        public class CategoryViewModel
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
                Name = categories.Name,
                Parent = categories.Parent != null ? new CategoryViewModel
                {
                    Id = categories.Parent.Id,
                    Name = categories.Parent.Name
                } : new CategoryViewModel
                {
                    Id = 0,
                    Name = "Inicio"
                },
                Products = categories.Products
            };
    }
}