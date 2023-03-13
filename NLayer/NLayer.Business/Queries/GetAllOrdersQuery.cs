using MediatR;
using NLayer.Business.Models.Order;

namespace NLayer.Business.Queries
{
    public class GetAllOrdersQuery : IRequest<IReadOnlyCollection<OrderDto>> { }
}
