using AutoMapper;
using MediatR;
using NLayer.Business.Commands;
using NLayer.Domain.Base;
using NLayer.Domain.Entities;

namespace NLayer.Business.Handlers.Command
{
    public class CreateItemHandler : IRequestHandler<CreateItemCommand>
    {
        private readonly IRepository<Item> itemRepository;
        private readonly IMapper mapper;

        public CreateItemHandler(IRepository<Item> itemRepository, IMapper mapper) {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        }

        public async Task Handle(CreateItemCommand request, CancellationToken cancellationToken) {
            var mappedItem = mapper.Map<Item>(request.Item);
            itemRepository.Add(mappedItem);
            await itemRepository.SaveAsync();
        }
    }
}
