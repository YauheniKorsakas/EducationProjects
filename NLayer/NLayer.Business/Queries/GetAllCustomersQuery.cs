using MediatR;
using NLayer.Business.Models;

namespace NLayer.Business.Queries
{
    public class GetAllCustomersQuery : IRequest<IReadOnlyCollection<CustomerDto>> { }
}
