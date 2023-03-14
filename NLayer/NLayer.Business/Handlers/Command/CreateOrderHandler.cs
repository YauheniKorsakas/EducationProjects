using MediatR;
using NLayer.Business.Commands;
using NLayer.Business.Models.Order;
using NLayer.Domain.Base;
using NLayer.Domain.Entities;

namespace NLayer.Business.Handlers.Command
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateOrderHandler(IUnitOfWork unitOfWork) {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken) {
            await unitOfWork.ExecuteInTransactionAsync(async () => {
                CheckIfCustomerExists(request.Order.CustomerId);
                CheckIfItemsExist(request.Order);
                CreateOrder(request.Order);
                ReduceTotalCountOfItems(request.Order);
                await unitOfWork.SaveAsync();
            });
        }

        private void ReduceTotalCountOfItems(OrderCreateDto order) {
            var itemsIds = order.Items.Select(s => s.Id);
            var itemsCount = unitOfWork
                .ItemRepository
                .Query
                .Where(s => itemsIds.Contains(s.Id))
                .Select(s => new { s.Id, s.TotalCount });

            foreach (var item in order.Items) {
                var itemToUpdate = new Item {
                    Id = item.Id,
                    TotalCount = itemsCount.First(s => s.Id == item.Id).TotalCount - item.Count
                };
                unitOfWork.ItemRepository.Update(itemToUpdate, s => s.TotalCount);
            }
        }

        private void  CreateOrder(OrderCreateDto order) {
            var newOrder = new Order {
                CustomerId = order.CustomerId,
                ItemOrders = order.Items
                    .Select(s => new ItemOrder {
                        ItemId = s.Id,
                        ItemCount = s.Count
                    }).ToList()
            };

            unitOfWork.OrderRepository.Add(newOrder);
        }

        private void CheckIfCustomerExists(int id) {
            var result = unitOfWork.CustomerRepository.Query.Any(s => s.Id == id);
            if (!result) {
                throw new InvalidOperationException($"Customer id {id} is invalid.");
            }
        }

        private void CheckIfItemsExist(OrderCreateDto order) {
            if (order.Items is null || !order.Items.Any()) {
                throw new InvalidOperationException($"Items do not exist");
            }

            var itemsIds = order.Items.Select(i => i.Id);
            var sourceItems = unitOfWork
                .ItemRepository
                .Query
                .Where(s => itemsIds.Contains(s.Id))
                .Select(s => new { s.Id, s.TotalCount })
                .ToList();
            
            if (sourceItems.Count() < order.Items.Count()) {
                throw new InvalidOperationException("Requested items are invalid.");
            }

            foreach (var item in sourceItems) {
                var requiredItem = order.Items.First(s => s.Id == item.Id);
                if (item.TotalCount <= requiredItem.Count) {
                    throw new InvalidOperationException($"" +
                        $"Item type {item.Id} has {item.TotalCount} items." +
                        $"Required amount is {requiredItem.Count}.");
                }
            }
        }
    }
}
