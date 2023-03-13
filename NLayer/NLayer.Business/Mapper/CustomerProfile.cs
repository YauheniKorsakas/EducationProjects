using AutoMapper;
using NLayer.Business.Models.Customer;
using NLayer.Domain.Entities;

namespace NLayer.Business.Mapper
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerCreateDto, Customer>();
        }
    }
}
