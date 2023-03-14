using NLayer.Domain.Entities;

namespace NLayer.Domain.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Order> OrderRepository { get; }
        IRepository<Customer> CustomerRepository { get; }
        IRepository<Item> ItemRepository { get; }
        Task ExecuteInTransactionAsync(Func<Task> action);
        Task SaveAsync();
    }
}
