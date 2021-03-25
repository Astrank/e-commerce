using EPE.Domain.Infrastructure;
using EPE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPE.Database
{
    public class CategoryManager : ICategoryManager
    {
        private readonly ApplicationDbContext _ctx;

        public CategoryManager(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        // CATEGORIES

        public Task<int> CreateCategory(Category productCategory)
        {
            _ctx.Categories.Add(productCategory);

            return _ctx.SaveChangesAsync();
        }

        public IEnumerable<TResult> GetCategories<TResult>(Func<Category, TResult> selector)
        {
            return _ctx.Categories.Select(selector);
        }

        public TResult GetCategoryWithProducts<TResult>(string name, Func<Category, TResult> selector)
        {
            return _ctx.Categories
                .Include(x => x.Subcategories)
                    .ThenInclude(y => y.Products)
                        .ThenInclude(z => z.Stock)
                .Where(x => x.Name == name)
                .Select(selector)
                .FirstOrDefault();
        }

        public TResult GetSubcategoryWithProducts<TResult>(string name, Func<Subcategory, TResult> selector)
        {
            return _ctx.Subcategories
                .Include(x => x.Category)
                .Include(x => x.Products)
                .Where(x => x.Name == name)
                .Select(selector)
                .FirstOrDefault();
        }

        public Task<int> UpdateCategory(Category productCategory)
        {
            _ctx.Categories.Update(productCategory);

            return _ctx.SaveChangesAsync();
        }

        public Task<int> DeleteCategory(int id)
        {
            var category = _ctx.Categories.FirstOrDefault(x => x.Id == id);

            _ctx.Categories.Remove(category);

            return _ctx.SaveChangesAsync();
        }

        // SUBCATEGORIES

        public Task<int> CreateSubcategory(Subcategory productSubcategory)
        {
            _ctx.Subcategories.Add(productSubcategory);

            return _ctx.SaveChangesAsync();
        }

        public IEnumerable<TResult> GetSubcategories<TResult>(Func<Subcategory, TResult> selector)
        {
            return _ctx.Subcategories.Select(selector);
        }

        public Task<int> UpdateSubcategory(Subcategory productSubcategory)
        {
            _ctx.Subcategories.Update(productSubcategory);

            return _ctx.SaveChangesAsync();
        }

        public Task<int> DeleteSubcategory(int id)
        {
            var subcategory = _ctx.Subcategories.FirstOrDefault(x => x.Id == id);

            _ctx.Subcategories.Remove(subcategory);

            return _ctx.SaveChangesAsync();
        }
    }
}
