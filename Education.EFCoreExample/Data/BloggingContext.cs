using Education.EFCoreExample.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Runtime;

namespace Education.EFCoreExample.Data
{
    internal class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ItemOrder> ItemOrders { get; set; } 

        private string connectionString = "";

        public BloggingContext(string connectionString) {
            this.connectionString = connectionString;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ItemOrder>()
                .HasKey(s => new { s.Id, s.OrderId, s.ItemId });
            modelBuilder.Entity<Order>()
                .Property(s => s.Date).HasColumnType("datetime");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(
                connectionString,
                providerOptions => {
                    providerOptions.EnableRetryOnFailure(2);
                });
            optionsBuilder.LogTo(Console.WriteLine);
        }
    }
}
