using EPE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPE.Domain.Infrastructure
{
    public interface ICategoryManager
    {
        Task<int> CreateCategory(Category productCategory);
        IEnumerable<TResult> GetCategories<TResult>(Func<Category, TResult> selector);
        TResult GetCategoryWithProducts<TResult>(string name, Func<Category, TResult> selector);
        TResult GetSubcategoryWithProducts<TResult>(string name, Func<Subcategory, TResult> selector);
        Task<int> UpdateCategory(Category productCategory);
        Task<int> DeleteCategory(int id);

        Task<int> CreateSubcategory(Subcategory productSubcategory);
        IEnumerable<TResult> GetSubcategories<TResult>(Func<Subcategory, TResult> selector);
        Task<int> UpdateSubcategory(Subcategory productSubcategory);
        Task<int> DeleteSubcategory(int id);
    }
}
