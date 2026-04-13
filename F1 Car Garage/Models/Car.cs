using System.ComponentModel.DataAnnotations;

namespace F1_Car_Garage.Models
{
    public class Car
    {
        public int CarId { get; set; }
        [Required, StringLength(100)]
        public string? Model { get; set; }
        public int BaseSpeed { get; set; }
        public int BaseHandling { get; set; }
        public int BaseAcceleration { get; set; }
        public int BaseManeuverability { get; set; }
        public ICollection<CarPart>? CarParts { get; set; }
    }
}
