using AutoMapper;
using MediatR;
using NLayer.Business.Commands;
using NLayer.Domain.Base;
using NLayer.Domain.Entities;

namespace NLayer.Business.Handlers.Command
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand>
    {
        private readonly IRepository<Customer> customerRepository;
        private readonly IMapper mapper;

        public CreateCustomerHandler(IRepository<Customer> customerRepository, IMapper mapper) {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task Handle(CreateCustomerCommand request, CancellationToken cancellationToken) {
            var mappedCustomer = mapper.Map<Customer>(request.Customer);
            customerRepository.Add(mappedCustomer);
            await customerRepository.SaveAsync();
        }
    }
}
