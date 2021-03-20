using EPE.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPE.Domain.Infrastructure
{
    public interface ICategoryManager
    {
        Task<int> CreateCategory(Category productCategory);
        IEnumerable<Category> GetCategories();
        Task<int> UpdateCategory(Category productCategory);
        Task<int> DeleteCategory(int id);

        Task<int> CreateSubcategory(Subcategory productSubcategory);
        IEnumerable<Subcategory> GetSubcategories();
        Task<int> UpdateSubcategory(Subcategory productSubcategory);
        Task<int> DeleteSubcategory(int id);
    }
}
