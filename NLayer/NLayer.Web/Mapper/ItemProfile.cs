using AutoMapper;
using NLayer.Business.Models.Item;
using NLayer.Web.Models.Item;

namespace NLayer.Web.Mapper
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<ItemDto, ItemViewModel>();
            CreateMap<ItemCreateViewModel, ItemCreateDto>();
            CreateMap<ItemUpdateViewModel, ItemUpdateDto>();
            CreateMap<ItemListDto, ItemListViewModel>();
            CreateMap<ItemOrderListDto, ItemOrderListViewModel>();
            CreateMap<ItemCreateOrderViewModel, ItemCreateOrderDto>();
        }
    }
}
