using Microsoft.EntityFrameworkCore;
using MultiShop.Models;

namespace MultiShop.DAL
{
    public class MultiShopDbContext:DbContext
    {
        public MultiShopDbContext(DbContextOptions<MultiShopDbContext> options):base(options)
        {
        }

        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }
    }
}
