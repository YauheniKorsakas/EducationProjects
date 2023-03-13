using AutoMapper;
using MediatR;
using NLayer.Business.Models.Order;
using NLayer.Business.Queries;
using NLayer.Domain.Base;
using NLayer.Domain.Entities;

namespace NLayer.Business.Handlers.Query
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, IReadOnlyCollection<OrderDto>>
    {
        private readonly IRepository<Order> orderRepository;
        private readonly IMapper mapper;

        public GetAllOrdersHandler(IRepository<Order> orderRepository, IMapper mapper) {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public Task<IReadOnlyCollection<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken) {
            var result = orderRepository.Get(s => true, s => s.).ToList();
            var mappedResult = mapper.Map<IReadOnlyCollection<OrderDto>>(result);

            return Task.FromResult(mappedResult);
        }
    }
}
