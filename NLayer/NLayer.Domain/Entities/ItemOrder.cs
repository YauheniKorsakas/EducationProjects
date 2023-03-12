namespace NLayer.Domain.Entities
{
    public class ItemOrder
    {
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public int ItemCount { get; set; }

        public Item Item { get; set; }
        public Order Order { get; set; }    }
}
