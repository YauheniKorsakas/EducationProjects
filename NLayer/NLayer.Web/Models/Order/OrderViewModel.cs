using NLayer.Web.Models.Item;

namespace NLayer.Web.Models.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; }
        public List<ItemViewModel> Items { get; set; }
    }
}
