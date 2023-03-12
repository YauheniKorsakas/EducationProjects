using NLayer.Domain.Base;

namespace NLayer.Domain.Entities
{
    public class Order : BaseEntity<int>
    {
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }        public ICollection<ItemOrder> ItemOrders { get; set; }    }
}
