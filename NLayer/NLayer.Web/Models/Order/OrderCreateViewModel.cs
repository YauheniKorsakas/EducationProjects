using DataAnnotationsExtensions;
using NLayer.Web.Models.Item;
using System.ComponentModel.DataAnnotations;

namespace NLayer.Web.Models.Order
{
    public class OrderCreateViewModel
    {
        [Min(1)]
        public int CustomerId { get; set; }

        [MinLength(1)]
        [Required]
        public List<ItemCreateOrderViewModel> Items { get; set; }
    }
}
