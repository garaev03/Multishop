﻿using Microsoft.EntityFrameworkCore;
using MultiShop.Models;

namespace MultiShop.DAL
{
    public class MultiShopDBContext:DbContext
    {
        public MultiShopDBContext(DbContextOptions<MultiShopDBContext> options):base(options){}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }    
        public DbSet<Image> Images { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }

    }
}