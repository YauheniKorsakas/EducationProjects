using NLayer.Business.Models.Item;

namespace NLayer.Business.Models.Order
{
    public class OrderCreateDto
    {
        public int CustomerId { get; set; }
        public List<ItemCreateOrderDto> Items { get; set; }
    }
}
