using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.ConstrainedExecution;

namespace F1_Car_Garage.Models
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
        public int BudgetId { get; set; }
        [ForeignKey("BudgetId")]
        public Budget? Budget { get; set; }
    }
}
