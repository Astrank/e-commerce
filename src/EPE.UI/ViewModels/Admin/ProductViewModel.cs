using Microsoft.AspNetCore.Http;

namespace EPE.UI.ViewModels
{
    public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public string Image { get; set; }

            public IFormFile ImageFile { get; set; }
        }
}