using NLayer.Business.Models.Item;

namespace NLayer.Business.Models.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; }
        public List<ItemListDto> Items { get; set; }
    }
}
