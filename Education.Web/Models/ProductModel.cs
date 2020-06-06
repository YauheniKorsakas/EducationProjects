using System.ComponentModel.DataAnnotations;

namespace Education.Web.Models {
    public class ProductModel {
        [Required]
        public string Name { get; set; }

        public ProductModel(string name) {
            Name = name;
        }

        public ProductModel() {

        }
    }
}
