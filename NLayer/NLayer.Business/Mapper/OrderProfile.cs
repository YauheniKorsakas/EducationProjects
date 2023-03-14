using AutoMapper;
using NLayer.Business.Models.Item;
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
                    o => o.MapFrom(order => $"{order.Customer.Name} {order.Customer.Surname}"))
                .ForMember(
                    s => s.Items,
                    o => o.MapFrom(order => order.ItemOrders.Select(s => new ItemOrderListDto { 
                        Id = s.ItemId,
                        Name = s.Item.Name,
                        Price = s.Item.Price,
                        ItemCount = s.ItemCount
                    }).ToList()));
        }
    }
}
