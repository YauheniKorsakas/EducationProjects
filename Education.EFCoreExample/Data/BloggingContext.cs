//using Education.EFCoreExample.Data.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using System.Runtime;

//namespace Education.EFCoreExample.Data
//{
//    internal class BloggingContext : DbContext
//    {
//        public DbSet<Blog> Blogs { get; set; }
//        public DbSet<Post> Posts { get; set; }

//        private string connectionString = "";

//        public BloggingContext(string connectionString)
//        {
//            this.connectionString = connectionString;
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder) {
//            base.OnModelCreating(modelBuilder);
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseSqlServer(
//                connectionString,
//                providerOptions => {
//                    providerOptions.EnableRetryOnFailure(2);
//                });
//            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
//        }
//    }
//}
