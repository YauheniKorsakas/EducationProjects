using MediatR;
using NLayer.Business.Models.Customer;

namespace NLayer.Business.Commands
{
    public class CreateCustomerCommand : IRequest
    {
        public CustomerCreateDto Customer { get; set; }
    }
}
