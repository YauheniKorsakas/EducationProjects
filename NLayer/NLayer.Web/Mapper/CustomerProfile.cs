using AutoMapper;
using NLayer.Business.Models.Customer;
using NLayer.Web.Models.Customer;

namespace NLayer.Web.Automapper
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDto, CustomerViewModel>();
            CreateMap<CustomerCreateViewModel, CustomerCreateDto>();
        }
    }
}
