using System.Collections.Generic;
using EPE.Application.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EPE.UI.Pages.Store
{
    public class IndexModel : PageModel
    {
        public IEnumerable<GetCategories.Response> Categories { get; set; }

        public void OnGet([FromServices] GetCategories getCategories)
        {
            Categories = getCategories.Do();
        }
    }
}
