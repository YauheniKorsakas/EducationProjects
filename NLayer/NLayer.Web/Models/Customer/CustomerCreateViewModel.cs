using System.ComponentModel.DataAnnotations;

namespace NLayer.Web.Models.Customer
{
    public class CustomerCreateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }
    }
}
