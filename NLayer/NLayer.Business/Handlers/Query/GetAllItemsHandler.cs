using AutoMapper;
using MediatR;
using NLayer.Business.Models.Item;
using NLayer.Business.Queries;
using NLayer.Domain.Base;
using NLayer.Domain.Entities;

namespace NLayer.Business.Handlers.Query
{
    public class GetAllItemsHandler : IRequestHandler<GetAllItemsQuery, IReadOnlyCollection<ItemDto>>
    {
        private readonly IRepository<Item> itemRepository;
        private readonly IMapper mapper;

        public GetAllItemsHandler(IRepository<Item> itemRepository, IMapper mapper) {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        }

        public Task<IReadOnlyCollection<ItemDto>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken) {
            var result = itemRepository.Query;
            var mappedResult = mapper.Map<IReadOnlyCollection<ItemDto>>(result);

            return Task.FromResult(mappedResult);
        }
    }
}
