using Microsoft.EntityFrameworkCore;

namespace Education.Auth.Domain
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public UserContext(DbContextOptions<UserContext> options): base(options) {
            Database.EnsureCreated();
        }
    }
}
