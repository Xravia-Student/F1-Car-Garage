using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1_Car_Garage.Models // The Part class represents a component or upgrade that can be added to a car, providing various performance boosts and having an associated cost. Each part is linked to a manufacturer, which produces the part and may have specific attributes or specialties in the context of the F1 Car Garage application.
{
    public class Part
    {
        public int PartId { get; set; }
        [Required, StringLength(100)]
        public string? Name { get; set; }
        [Required]
        public string? Type { get; set; }
        [Required, Range(0.01, 10000000)]
        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }
        [Range(0, 50)] public int SpeedBoost { get; set; }
        [Range(0, 50)] public int HandlingBoost { get; set; }
        [Range(0, 50)] public int AccelBoost { get; set; }
        [Range(0, 50)] public int ManeuverabilityBoost { get; set; }
        [Required]
        public int ManufacturerId { get; set; }
        [ForeignKey("ManufacturerId")]
        public Manufacturer? Manufacturer { get; set; }
    }
}
