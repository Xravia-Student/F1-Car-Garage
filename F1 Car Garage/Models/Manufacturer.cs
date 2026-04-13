using System.ComponentModel.DataAnnotations;

namespace F1_Car_Garage.Models
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
    }
}
