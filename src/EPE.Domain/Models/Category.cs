using System.Collections.Generic;

namespace EPE.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }   
        public string Name { get; set; }
        public IEnumerable<Subcategory> Subcategories { get; set; }
    }
}