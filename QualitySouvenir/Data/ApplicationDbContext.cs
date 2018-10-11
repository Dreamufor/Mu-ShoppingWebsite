using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QualitySouvenir.Models;

namespace QualitySouvenir.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Souvenir> Souvenirs { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<CartItem> CartItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Souvenir>().ToTable("Souvenir");

            modelBuilder.Entity<Supplier>().ToTable("Supplier");

            modelBuilder.Entity<Category>().ToTable("Category");

            modelBuilder.Entity<OrderItem>().ToTable("OrderItem");

            modelBuilder.Entity<Order>().ToTable("Order");

            modelBuilder.Entity<CartItem>().ToTable("CartItem");

            modelBuilder.Entity<OrderItem>().HasOne(p => p.Order).WithMany(o => o.OrderItems).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Souvenir>().HasOne(p => p.Supplier).WithMany(o => o.Souvenirs).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Souvenir>().HasOne(p => p.Category).WithMany(o => o.Souvenirs).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>().HasOne(p => p.User).WithMany(o => o.Orders).OnDelete(DeleteBehavior.Cascade);

        }

        public DbSet<QualitySouvenir.Models.ShoppingCart> ShoppingCart { get; set; }
    }
}
