using EPE.Application.CategoriesAdmin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EPE.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class SubcategoriesController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> CreateSubcategory(
            [FromServices] CreateSubcategory createSubcategory,
            CreateSubcategory.Request request) =>
                Ok(await createSubcategory.Do(request));

        [HttpGet]
        public IActionResult GetSubcategories([FromServices] GetSubcategories getSubcategories) =>
            Ok(getSubcategories.Do());

        [HttpPut]
        public async Task<IActionResult> UpdateSubcategory(
            [FromServices] UpdateSubcategory updateSubcategory,
            UpdateSubcategory.SubcategoryViewModel vm) =>
                Ok(await updateSubcategory.Do(vm));

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory([FromServices] DeleteSubcategory deleteSubcategory, int id) =>
            Ok(deleteSubcategory.Do(id));
    }
}
