using System.ComponentModel.DataAnnotations;

namespace Education.Web.Models {
    public class PersonModel {
        [Required]
        public string Name { get; set; }
    }
}
