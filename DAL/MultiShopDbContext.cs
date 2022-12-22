using Microsoft.EntityFrameworkCore;
using MultiShop.Models;

namespace MultiShop.DAL
{
    public class MultiShopDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-KA8SSD4;Database=MultiShopDB;Trusted_Connection=true;");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }
    }
}
