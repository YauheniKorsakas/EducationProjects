using NLayer.Domain.Base;

namespace NLayer.Domain.Entities
{
    public class Item : BaseEntity<int>
    {
        public string Name { get; set; }
        public int TotalCount { get; set; }
        public double Price { get; set; }
    }
}
