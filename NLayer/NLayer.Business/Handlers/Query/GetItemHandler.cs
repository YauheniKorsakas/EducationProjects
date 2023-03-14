using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using NLayer.Business.Models.Item;
using NLayer.Business.Queries;
using NLayer.Domain.Base;
using NLayer.Domain.Entities;

namespace NLayer.Business.Handlers.Query
{
    public class GetItemHandler : IRequestHandler<GetItemQuery, ItemDto>
    {
        private readonly IRepository<Item> itemRepository;
        private readonly IMapper mapper;

        public GetItemHandler(IRepository<Item> itemRepository, IMapper mapper) {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        }

        public Task<ItemDto> Handle(GetItemQuery request, CancellationToken cancellationToken) {
            var result = itemRepository
                .Query
                .Where(item => item.Id == request.Id)
                .ProjectTo<ItemDto>(mapper.ConfigurationProvider)
                .FirstOrDefault();

            return Task.FromResult(result);
        }
    }
}
