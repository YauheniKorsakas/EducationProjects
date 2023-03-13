using AutoMapper;
using NLayer.Business.Models.Order;
using NLayer.Domain.Entities;

namespace NLayer.Business.Mapper
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(
                    s => s.CustomerFullName,
                    o => o.MapFrom(order =>  $"{order.Customer.Name} {order.Customer.Surname}"));
        }
    }
}
