using DataAnnotationsExtensions;

namespace NLayer.Web.Models.Item
{
    public class ItemCreateOrderViewModel
    {
        [Min(1)]
        public int Id { get; set; }

        [Min(1)]
        public int Count { get; set; }
    }
}
