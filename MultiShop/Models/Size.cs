namespace MultiShop.Models
{
    public class Size
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public bool isDeleted { get; set; }
        public List<ProductSizeColor> ProductSizeColors { get; set; }
        public Size()
        {
            ProductSizeColors = new();
        }
    }
}
