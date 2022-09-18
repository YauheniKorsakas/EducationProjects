using System.ComponentModel.DataAnnotations;

namespace Education.SwaggerPlayground.ViewModels
{
    public class PersonViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
