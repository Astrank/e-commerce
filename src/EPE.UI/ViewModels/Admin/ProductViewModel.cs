using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace EPE.UI.ViewModels
{
    public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public string PrimaryImage { get; set; }
            public List<string> Images { get; set; }

            public IFormFile PrimaryImageFile { get; set; }
            public List<IFormFile> ImageFiles { get; set; }
        }
}