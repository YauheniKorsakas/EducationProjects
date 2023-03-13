using AutoMapper;
using NLayer.Business.Models.Item;
using NLayer.Domain.Entities;

namespace NLayer.Business.Mapper
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemDto>();
            CreateMap<ItemCreateDto, Item>();
            CreateMap<ItemUpdateDto, Item>();
        }
    }
}
