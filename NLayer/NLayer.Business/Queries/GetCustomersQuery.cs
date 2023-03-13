using MediatR;
using NLayer.Business.Models.Customer;

namespace NLayer.Business.Queries
{
    public class GetCustomerQuery : IRequest<CustomerDto>
    {
        public int Id { get; set; }
    }
}