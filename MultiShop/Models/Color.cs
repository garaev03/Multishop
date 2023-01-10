namespace MultiShop.Models
{
    public class Color
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public bool isDeleted { get; set; }
        public List<ProductColor> ProductColors { get; set; }
        public Color()
        {
            ProductColors = new();
        }

    }
}
