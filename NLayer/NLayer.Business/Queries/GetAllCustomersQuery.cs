using MediatR;
using NLayer.Business.Models.Customer;

namespace NLayer.Business.Queries
{
    public class GetAllCustomersQuery : IRequest<IReadOnlyCollection<CustomerDto>> { }
}
