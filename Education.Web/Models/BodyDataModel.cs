using System.ComponentModel.DataAnnotations;

namespace Education.Web.Models {
    public class BodyDataModel {
        [Required]
        public string Value { get; set; }
    }
}
