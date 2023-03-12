using NLayer.Domain.Base;
using NLayer.Domain.Entities;

namespace NLayer.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork // could be split by grouping repos
    {
        private readonly ShopContext context;
        public IRepository<Order> OrderRepository { get; }
        public IRepository<Customer> CustomerRepository { get; }
        public IRepository<Item> ItemRepository { get; }

        public UnitOfWork(
            ShopContext context,
            IRepository<Order> orderRepository,
            IRepository<Customer> customerRepository,
            IRepository<Item> itemRepository)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            OrderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            CustomerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            ItemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        }

        public void Dispose() {
            Console.WriteLine("Disposed");
        }

        public Task SaveAsync() {
            return context.SaveChangesAsync();
        }
    }
}
