using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BookstoreAPI.Models;

namespace BookstoreAPI.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        // DbSet for your entities
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Inventory> Inventory { get; set; }

        // New DbSet properties for Cart and Wishlist
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        
        // If you have more entities like Authors, Orders, etc., add them here as well
        // public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Additional configurations can go here
            // Example: builder.Entity<Book>().HasIndex(b => b.ISBN).IsUnique();


            // Composite key for OrderDetail
            builder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderId, od.BookId });
            
            
            // Database Relationships Overview
            
            // Books - Authors:
            // One-to-many: One Author can have many Books.
            
            // Orders - Customers:
            // One-to-many: One Customer can place many Orders.
            
            // Books - Orders:
            // Many-to-many: A book can appear in multiple orders, and an order can contain multiple books. This is implemented through the OrderDetails junction table.
            
            // Books - Reviews:
            // One-to-many: A Book can have many Reviews, and each review is linked to a Customer.
            
            // Books - Inventory:
            // One-to-one: Each Book has one corresponding entry in the Inventory table to track stock levels.


            // Define relationships
            builder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId);

            builder.Entity<Book>()
                .HasMany(b => b.OrderDetails)
                .WithOne(od => od.Book)
                .HasForeignKey(od => od.BookId);

            builder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId);

            builder.Entity<Book>()
                .HasMany(b => b.Reviews)
                .WithOne(r => r.Book)
                .HasForeignKey(r => r.BookId);

            builder.Entity<Inventory>()
                .HasOne(i => i.Book)
                .WithOne(b => b.Inventory)
                .HasForeignKey<Inventory>(i => i.BookId);

            // Define relationships for new entities

            // One-to-many: One Customer can have many items in their Wishlist
            builder.Entity<Wishlist>()
                .HasOne(w => w.Customer)
                .WithMany(c => c.Wishlists)
                .HasForeignKey(w => w.CustomerId);

            // One-to-many: One Customer can have many items in their Cart
            builder.Entity<Cart>()
                .HasOne(c => c.Customer)
                .WithMany(cu => cu.Carts)
                .HasForeignKey(c => c.CustomerId);

            // One-to-many: One Book can be in many Wishlists
            builder.Entity<Wishlist>()
                .HasOne(w => w.Book)
                .WithMany(b => b.Wishlists)
                .HasForeignKey(w => w.BookId);

            // One-to-many: One Book can be in many Carts
            builder.Entity<Cart>()
                .HasOne(c => c.Book)
                .WithMany(b => b.Carts)
                .HasForeignKey(c => c.BookId);
        }
    }
}
