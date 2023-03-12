namespace Education.EFCoreExample.Data.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalCount { get; set; }
        public double Price { get; set; }

        public ICollection<ItemOrder> ItemOrders { get; set; }
    }
}
