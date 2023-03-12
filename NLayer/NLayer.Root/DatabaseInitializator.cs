using NLayer.Infrastructure;

namespace NLayer.Root
{
    public static class DatabaseInitializator
    {
        public static async Task InitializeAndSeedAsync(ShopContext context) {
            using (context) {
                await DbInitializer.InitializeAsync(context);
                await DbInitializer.SeedAsync(context);
            }
        }
    }
}
