using AutoMapper;
using NLayer.Business.Models;
using NLayer.Web.Models.Query.Customer;

namespace NLayer.Web.Automapper
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDto, CustomerViewModel>();
        }
    }
}
