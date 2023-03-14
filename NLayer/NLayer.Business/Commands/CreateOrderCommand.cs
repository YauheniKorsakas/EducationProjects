using MediatR;
using NLayer.Business.Models.Order;

namespace NLayer.Business.Commands
{
    public class CreateOrderCommand : IRequest
    {
        public OrderCreateDto Order { get; set; }
    }
}
