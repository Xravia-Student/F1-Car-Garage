using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1_Car_Garage.Models // The Manufacturer class represents a car manufacturer in the F1 Car Garage application, with properties for the manufacturer's name, country, tier, and a collection of parts associated with the manufacturer. It also includes a reference to the user who created or manages the manufacturer entry, allowing for user-specific data management and access control within the application.
{
    public class Racer
    {
        public int RacerId { get; set; }
        [Required, StringLength(100)]
        public string? Name { get; set; }
        [Required, StringLength(100)]
        public string? Nationality { get; set; }
        public int Points { get; set; }
        public int CarId { get; set; }
        [ForeignKey("CarId")]
        public Car? Car { get; set; }

        // made optional to avoid inserting default 0 which violates FK when no Budget exists
        public int? BudgetId { get; set; }
        [ForeignKey("BudgetId")]
        public Budget? Budget { get; set; }

        // Reference to the associated Identity user
        public string? UserId { get; set; }
    }
}
