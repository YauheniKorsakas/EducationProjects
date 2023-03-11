using System.ComponentModel.DataAnnotations;

namespace NLayer.Web.Models.Command.Order
{
    public class OrderCreateViewModel
    {
        [Required]
        public int CustomerId { get; set; }

        [MinLength(1)]
        [Required]
        public Dictionary<int, int> ItemsAndCounts { get; set; }
    }
}
