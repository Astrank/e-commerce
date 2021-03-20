using EPE.Domain.Infrastructure;
using System.Threading.Tasks;

namespace EPE.Application.CategoriesAdmin
{
    [Service]
    public class DeleteSubcategory
    {
        private readonly ICategoryManager _categoryManager;

        public DeleteSubcategory(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public async Task Do(int id)
        {
            if (await _categoryManager.DeleteSubcategory(id) <= 0)
            {
                throw new System.Exception("ERROR DELETING SUBCATEGORY");
            };
        }
    }
}