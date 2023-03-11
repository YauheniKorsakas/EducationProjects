using NLayer.Web.Models.Query.Item;

namespace NLayer.Web.Models.Query.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; }
        public List<ItemViewModel> Items { get; set; }
    }
}
