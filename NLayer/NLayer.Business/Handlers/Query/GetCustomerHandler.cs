using AutoMapper;
using MediatR;
using NLayer.Business.Models.Customer;
using NLayer.Business.Queries;
using NLayer.Domain.Base;
using NLayer.Domain.Entities;

namespace NLayer.Business.Handlers.Query
{
    public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, CustomerDto>
    {
        private readonly IRepository<Customer> customerRepository;
        private readonly IMapper mapper;

        public GetCustomerHandler(IRepository<Customer> customerRepository, IMapper mapper) {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        // Do we need to check request nullability here?
        public Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken) {
            var result = customerRepository.Get(request.Id);
            var mappedResult = mapper.Map<CustomerDto>(result);

            return Task.FromResult(mappedResult);
        }
    }
}
