using System.Runtime.ConstrainedExecution;

namespace F1_Car_Garage.Models
{
    public class CarPart
    {
        public int CarId { get; set; }
        public Car? Car { get; set; }
        public int PartId { get; set; }
        public Part? Part { get; set; }
        public DateTime InstalledOn { get; set; } = DateTime.Now;
    }
}
