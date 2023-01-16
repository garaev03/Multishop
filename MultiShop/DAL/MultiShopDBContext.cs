using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiShop.Models;

namespace MultiShop.DAL
{
    public class MultiShopDBContext:IdentityDbContext<AppUser>
    {
        public MultiShopDBContext(DbContextOptions<MultiShopDBContext> options):base(options){}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }    
        public DbSet<Image> Images { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ProductSizeColor> ProductSizeColors { get; set; }
    }
}
