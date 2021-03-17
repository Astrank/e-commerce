using System.Collections.Generic;

namespace EPE.Domain.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }   
        public string Name { get; set; }
        public IEnumerable<ProductSubcategory> Subcategories { get; set; }
    }
}