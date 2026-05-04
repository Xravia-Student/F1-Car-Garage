using System.Runtime.ConstrainedExecution;

namespace F1_Car_Garage.Models // CarPart is a join entity for the many-to-many relationship between Car and Part, with an additional property InstalledOn to track when a part was installed on a car
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
