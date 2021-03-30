using EPE.Domain.Infrastructure;
using EPE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EPE.Application.Categories
{
    [Service]
    public class GetCategory
    {
        private readonly ICategoryManager _categoryManager;

        public GetCategory(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public class Response
        {
            public string Name { get; set; }
            public IEnumerable<string> Parents { get; set; }
            public IEnumerable<string> Subcategories { get; set; }
        }

        public class ParentViewModel
        {
            public string Name { get; set; }
        }

        public class SubcategoriesViewModel
        {
            public string Name { get; set; }
        }

        public Response Do(string name)
        {
            var categories = _categoryManager.GetCategory(Projection, name);

            var parents = new List<string>();
            var children = new List<string>();

            var categoryIndex = categories.FindIndex(x => x == name);

            if (categoryIndex > 0)
            {
                for (int i = 0; i < categoryIndex; i++)
                {
                    parents.Add(categories[i]);
                }
            }

            if (categories.Count > categoryIndex)
            {
                for (int i = categoryIndex + 1; i < categories.Count; i++)
                {
                    children.Add(categories[i]);
                }
            }

            return new Response
            {
                Name = categories[categoryIndex],
                Parents = parents,
                Subcategories = children
            };
        }

        private static Func<Category, string> Projection = (category) => category.Name;
    }
}