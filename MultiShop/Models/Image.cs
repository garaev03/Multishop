namespace MultiShop.Models
{
    public class Image
    {
        public int Id { get; set; } 
        public string Path { get; set; }
        public bool isDeleted { get; set; }
        public int ProductId { get; set; }
        public bool isMain { get; set; }
        public Product Product { get; set; }
        public Image()
        {
            Product = new();
        }
    }
}
