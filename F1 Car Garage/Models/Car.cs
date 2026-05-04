using System.ComponentModel.DataAnnotations;

namespace F1_Car_Garage.Models
{
    public class Car // The Car class represents a Formula 1 car in the garage, with properties for its unique identifier, model name, and base performance attributes such as speed, handling, acceleration, and maneuverability. It also has a collection of CarPart objects that represent the parts installed on the car, allowing for dynamic modification of the car's performance based on the parts used.
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
