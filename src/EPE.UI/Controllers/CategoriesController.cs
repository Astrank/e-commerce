using EPE.Application.CategoriesAdmin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EPE.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class CategoriesController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> CreateCategory(
            CreateCategory.Request request,
            [FromServices] CreateCategory createCategory) =>
                Ok(await createCategory.Do(request));

        [HttpGet]
        public IActionResult GetCategories([FromServices] GetCategories getCategories)
        {
            var categories = getCategories.Do();

            return Ok(categories);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(
            UpdateCategory.CategoryViewModel vm,
            [FromServices] UpdateCategory updateCategory) =>
                Ok(await updateCategory.Do(vm));

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory([FromServices] DeleteCategory deleteCategory, int id) =>
            Ok(deleteCategory.Do(id));
    }
}