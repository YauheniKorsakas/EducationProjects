using MediatR;
using NLayer.Business.Models;
using NLayer.Business.Queries;

namespace NLayer.Business.Handlers
{
    public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, IReadOnlyCollection<CustomerDto>>
    {
        public Task<IReadOnlyCollection<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken) {
            return Task.FromResult(new List<CustomerDto> { 
                new CustomerDto {
                    Id = 1,
                    Name = "Zheka",
                    Surname = "Korsakas"
                }
            } as IReadOnlyCollection<CustomerDto>);
        }
    }
}
