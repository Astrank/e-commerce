using EPE.Domain.Infrastructure;
using EPE.Domain.Models;
using System.Threading.Tasks;

namespace EPE.Application.CategoriesAdmin
{
    [Service]
    public class UpdateCategory
    {
        private readonly ICategoryManager _categoryManager;

        public UpdateCategory(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public class CategoryViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public async Task<CategoryViewModel> Do(CategoryViewModel vm)
        {
            var category = new Category
            {
                Id = vm.Id,
                Name = vm.Name
            };

            if (await _categoryManager.UpdateCategory(category) <= 0)
            {
                throw new System.Exception("ERROR UPDATING CATEGORY");
            };

            return new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}