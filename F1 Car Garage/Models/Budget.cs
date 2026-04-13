using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;

namespace F1_Car_Garage.Models
{
    public class Budget
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