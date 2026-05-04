using System.ComponentModel.DataAnnotations;

namespace F1_Car_Garage.Models //stands for the namespace of the project, and it is used to organize the classes and to avoid naming conflicts between classes with the same name in different namespaces
{
    public class Manufacturer
    {
        public int ManufacturerId { get; set; }
        [Required, StringLength(100)]
        public string? Name { get; set; }
        [Required, StringLength(100)]
        public string? Country { get; set; }
        [Required]
        public string? Tier { get; set; }
        public ICollection<Part>? Parts { get; set; }

        // Reference the Identity of the user
        public string? UserId { get; set; }
    }
}
