using System.Threading.Tasks;
using EPE.Application.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPE.UI.Pages.Store
{
    public class ProductsModel : PageModel
    {
        public GetCategoryWithProducts.Response Category { get; set; }

        public void OnGet([FromServices] GetCategoryWithProducts getCategoryWithProducts, string name)
        {
            Category = getCategoryWithProducts.Do(name);
        }
    }
}