using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace NLayer.Web.Models.Item
{
    public class ItemCreateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Min(1)]
        public int TotalCount { get; set; }

        [Min(1)]
        public int Price { get;set; }
    }
}
