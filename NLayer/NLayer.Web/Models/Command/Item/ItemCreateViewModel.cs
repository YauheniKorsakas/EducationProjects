using System.ComponentModel.DataAnnotations;

namespace NLayer.Web.Models.Command.Item
{
    public class ItemCreateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Count { get; set; }
    }
}
