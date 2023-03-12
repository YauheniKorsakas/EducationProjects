namespace Education.EFCoreExample.Data.Entities
{
    public class ItemOrder
    {
        public Guid Id { get; set; }
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public int ItemCount { get; set; }

        public Item Item { get; set; }
        public Order Order { get; set; }
    }
}
