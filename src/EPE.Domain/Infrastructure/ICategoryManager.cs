using EPE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPE.Domain.Infrastructure
{
    public interface ICategoryManager
    {
        Task<int> CreateCategory(Category productCategory);
        IEnumerable<TResult> GetMainCategories<TResult>(Func<Category, TResult> selector);
        IEnumerable<TResult> GetCategories<TResult>(Func<Category, TResult> selector);
        List<TResult> GetCategory<TResult>(Func<Category, TResult> selector, string name);
        Task<int> UpdateCategory(Category productCategory);
        Task<int> DeleteCategory(int id);
    }
}
