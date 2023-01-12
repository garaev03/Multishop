namespace MultiShop.Models
{
    public class ProductSizeColor
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public Product product { get; set; }
        public Size size { get; set; }
        public Color color { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public ProductSizeColor()
        {
            product = new();
            size = new();
            color = new();
        }
    }
}
