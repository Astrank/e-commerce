using EPE.Domain.Infrastructure;
using EPE.Domain.Models;
using System.Threading.Tasks;

namespace EPE.Application.CategoriesAdmin
{
    [Service]
    public class UpdateSubcategory
    {
        private readonly ICategoryManager _categoryManager;

        public UpdateSubcategory(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public class SubcategoryViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int CategoryId { get; set; }
        }

        public async Task<SubcategoryViewModel> Do(SubcategoryViewModel vm)
        {
            var subcategory = new Subcategory
            {
                Id = vm.Id,
                Name = vm.Name,
                CategoryId = vm.CategoryId
            };

            if (await _categoryManager.UpdateSubcategory(subcategory) <= 0)
            {
                throw new System.Exception("ERROR UPDATING SUBCATEGORY");
            };
            
            return new SubcategoryViewModel
            {
                Id = subcategory.Id,
                Name = subcategory.Name,
                CategoryId = subcategory.CategoryId
            };
        }
    }
}