using AutoMapper;
using MediatR;
using NLayer.Business.Commands;
using NLayer.Domain.Base;
using NLayer.Domain.Entities;
using System.Linq.Expressions;

namespace NLayer.Business.Handlers.Command
{
    public class UpdateItemHandler : IRequestHandler<UpdateItemCommand>
    {
        private readonly IRepository<Item> itemRepository;
        private readonly IMapper mapper;

        public UpdateItemHandler(IRepository<Item> itemRepository, IMapper mapper) {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        }

        public async Task Handle(UpdateItemCommand request, CancellationToken cancellationToken) {
            var mappedItem = mapper.Map<Item>(request.Item);
            var propMap = new List<(object requestProp, Expression<Func<Item, object>> entityProp)> {
                (request.Item.Name, s => s.Name),
                (request.Item.TotalCount, s => s.TotalCount),
                (request.Item.Price, s => s.Price)
            };
            var propsToUpdate = propMap.Where(s => s.requestProp != null).Select(s => s.entityProp).ToArray();

            itemRepository.Update(mappedItem, propsToUpdate);
            await itemRepository.SaveAsync();
        }
    }
}
