using AutoMapper;
using MediatR;
using NLayer.Business.Models.Customer;
using NLayer.Business.Queries;
using NLayer.Domain.Base;
using NLayer.Domain.Entities;

namespace NLayer.Business.Handlers.Query
{
    public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, IReadOnlyCollection<CustomerDto>>
    {
        private readonly IRepository<Customer> customerRepository;
        private readonly IMapper mapper;

        public GetAllCustomersHandler(IRepository<Customer> customerRepository, IMapper mapper) {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public Task<IReadOnlyCollection<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken) {
            var result = customerRepository.Query;
            var mappedResult = mapper.Map<IReadOnlyCollection<CustomerDto>>(result);

            return Task.FromResult(mappedResult);
        }
    }
}
