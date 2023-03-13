using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace NLayer.Web.Models.Item
{
    public class ItemUpdateViewModel
    {
        [Min(1)]
        public int Id { get; set; }

        public string Name { get; set; }
        public int? TotalCount { get; set; }
        public double? Price { get; set; }
    }
}
