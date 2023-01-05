using Microsoft.EntityFrameworkCore;
using MultiShop.Models;

namespace MultiShop.DAL
{
    public class MultiShopDbContext:DbContext
    {
        public MultiShopDbContext(DbContextOptions<MultiShopDbContext> options):base(options)
        {
        }
<<<<<<< HEAD:DAL/MultiShopDbContext.cs

=======
                
>>>>>>> ec35ff3f76ebb8d402d8141ea8c692509819ae79:App/DAL/MultiShopDbContext.cs
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
