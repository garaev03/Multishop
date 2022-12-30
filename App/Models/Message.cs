using Microsoft.Build.Framework;

namespace MultiShop.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string Subject { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
