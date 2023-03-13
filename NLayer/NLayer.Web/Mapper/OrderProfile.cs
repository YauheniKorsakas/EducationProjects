using AutoMapper;
using NLayer.Business.Models.Order;
using NLayer.Web.Models.Order;

namespace NLayer.Web.Mapper
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDto, OrderViewModel>()
                .ForMember(s => s.TotalPrice, o => o.MapFrom(item => item.Items.Sum(s => s.Price)));
        }
    }
}
