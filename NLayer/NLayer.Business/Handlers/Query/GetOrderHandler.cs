using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using NLayer.Business.Models.Order;
using NLayer.Business.Queries;
using NLayer.Domain.Base;
using NLayer.Domain.Entities;

namespace NLayer.Business.Handlers.Query
{
    public class GetOrderHandler : IRequestHandler<GetOrderQuery, OrderDto>
    {
        private readonly IRepository<Order> orderRepository;
        private readonly IMapper mapper;

        public GetOrderHandler(IRepository<Order> orderRepository, IMapper mapper) {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public Task<OrderDto> Handle(GetOrderQuery request, CancellationToken cancellationToken) {
            var result = orderRepository
                .Query
                .Where(s => s.Id == request.Id)
                .ProjectTo<OrderDto>(mapper.ConfigurationProvider)
                .FirstOrDefault();

            return Task.FromResult(result);
        }
    }
}
