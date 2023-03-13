using Microsoft.EntityFrameworkCore;
using NLayer.Domain.Entities;

namespace NLayer.Infrastructure
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ItemOrder> ItemOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ItemOrder>().HasKey(s => new { s.OrderId, s.ItemId });
        }
    }
}
