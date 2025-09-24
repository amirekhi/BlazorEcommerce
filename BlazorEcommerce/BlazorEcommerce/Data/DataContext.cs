using ClassLibrary1.Entities; // Make sure this includes your `User` entity
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorEcommerce.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<VisitLog> VisitLogs { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "d60c5b44-34e7-49ef-a0c5-763a98b6e74f", // Static GUID
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "d60c5b44-34e7-49ef-a0c5-763a98b6e74f" // Optional, but useful
                },
                new IdentityRole
                {
                    Id = "bb2b129d-9ae2-4c99-9fcd-7f72f774a2d2", // Static GUID
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "bb2b129d-9ae2-4c99-9fcd-7f72f774a2d2" // Optional
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            builder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Electronics",
                    CreatedAt = new DateTime(2025, 6, 2, 12, 0, 0, DateTimeKind.Utc)
                },
                new Category
                {
                    Id = 2,
                    Name = "Books",
                    CreatedAt = new DateTime(2025, 6, 2, 12, 0, 0, DateTimeKind.Utc)
                },
                new Category
                {
                    Id = 3,
                    Name = "Clothing",
                    CreatedAt = new DateTime(2025, 6, 2, 12, 0, 0, DateTimeKind.Utc)
                });


            builder.Entity<Cart>()
                 .HasMany(c => c.Items)
                 .WithOne(i => i.Cart)
                 .HasForeignKey(i => i.CartId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CartItem>()
                .HasKey(i => new { i.CartId, i.ProductId }); // composite key


            builder.Entity<Order>()
               .HasMany(o => o.Items)
               .WithOne(i => i.Order)
               .HasForeignKey(i => i.OrderId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProductImage>()
            .HasOne<Product>()  // No need for navigation on ProductImage
            .WithMany(p => p.Images)
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
