using MediatR;
using NLayer.Business.Models.Order;

namespace NLayer.Business.Queries
{
    public class GetOrderQuery : IRequest<OrderDto>
    {
        public int Id { get; set; }
    }
}
