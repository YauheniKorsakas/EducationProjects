using NLayer.Domain.Entities;

namespace NLayer.Infrastructure
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(ShopContext context) {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }

        public static async Task SeedAsync(ShopContext context) {
            context.AddRange(new Customer[] {
                new Customer {
                    Name = "Zheka",
                    Surname = "Top"
                },
                new Customer {
                    Name = "Artsiom",
                    Surname = "Kuis"
                },
                new Customer {
                    Name = "Sergey",
                    Surname = "Kononovich"
                }
            });
            context.AddRange(new Item[] {
                new Item {
                    Name = "IPhone",
                    Price = 1000,
                    TotalCount = 1123
                },
                new Item {
                    Name = "Samsung A50",
                    Price = 200,
                    TotalCount = 100
                },
                new Item {
                    Name = "Tablet S8 lite",
                    Price = 1500,
                    TotalCount = 34
                },
                new Item {
                    Name = "Xiaomi",
                    Price = 200,
                    TotalCount = 12323
                },
            });
            context.Orders.Add(new Order {
                CustomerId = 1,
                ItemOrders = new List<ItemOrder> {
                    new ItemOrder {
                        ItemId = 1,
                        OrderId = 1,
                        ItemCount = 1
                    }
                }
            });
            await context.SaveChangesAsync();
        }
    }
}
