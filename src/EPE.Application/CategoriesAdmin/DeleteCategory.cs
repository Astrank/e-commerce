using EPE.Domain.Infrastructure;
using System.Threading.Tasks;

namespace EPE.Application.CategoriesAdmin
{
    [Service]
    public class DeleteCategory
    {
        private readonly ICategoryManager _categoryManager;

        public DeleteCategory(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public async Task Do(int id)
        {
            if (await _categoryManager.DeleteCategory(id) <= 0)
            {
                throw new System.Exception("ERROR DELETING CATEGORY"); 
            };
        }
    }
}