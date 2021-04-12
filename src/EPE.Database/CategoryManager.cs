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

        public Task<int> CreateCategory(Category category)
        {
            _ctx.Categories.Add(category);

            return _ctx.SaveChangesAsync();
        }

        public IEnumerable<TResult> GetMainCategories<TResult>(Func<Category, TResult> selector)
        {
            var categories = _ctx.Categories
                .Where(x => x.ParentId == null)
                .Select(selector)
                .ToList();

            return categories;
        }

        public IEnumerable<TResult> GetCategories<TResult>(Func<Category, TResult> selector)
        {
            var categories = _ctx.Categories
                .Select(selector)
                .ToList();

            return categories;
        }

        public List<TResult> GetCategory<TResult>(Func<Category, TResult> selector, string name)
        {
            var hierarchy = _ctx.Categories
                .FromSqlInterpolated($"SELECT * FROM sp_getcategoryhierarchy({name})")
                .Select(selector)
                .ToList();

            return hierarchy;
        }

        public Task<int> UpdateCategory(Category category)
        {
            _ctx.Categories.Update(category);

            return _ctx.SaveChangesAsync();
        }

        public async Task<int> DeleteCategory(int id)
        {
            var category = _ctx.Categories.FirstOrDefault(x => x.Id == id);

            if (category != null)
            {
                await DeleteChildren(id);
                _ctx.Categories.Remove(category);
            }

            return await _ctx.SaveChangesAsync();
        }

        async Task DeleteChildren(int id)
        {
            var children =  _ctx.Categories.Where(c => c.ParentId == id);
            
            foreach (var child in children)
            {
                await DeleteChildren(child.Id);
                _ctx.Categories.Remove(child);
            }
        }
    }
}
