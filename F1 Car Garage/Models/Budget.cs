using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;

namespace F1_Car_Garage.Models
{
    public class Budget // The Budget class represents the financial resources allocated to a racer for their racing activities, including the total budget, amount spent, sponsorship contributions, and the remaining budget. It is associated with a Racer and helps manage the financial aspects of racing operations within the garage.
    {
        public int BudgetId { get; set; }
        [Required, Range(0, 100000000)]
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }
        [DataType(DataType.Currency)]
        public decimal Spent { get; set; }
        [DataType(DataType.Currency)]
        public decimal Sponsorship { get; set; }
        public decimal Remaining => Total + Sponsorship - Spent;
    }
}
// Its to tie in with maps and winning races allowing for racers to increase their budget through merit.