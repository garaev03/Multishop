using Microsoft.Build.Framework;
using System.Xml.Schema;

namespace MultiShop.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Image { get; set; }     
        public List<Product>? Products { get; set; }
    }
}
