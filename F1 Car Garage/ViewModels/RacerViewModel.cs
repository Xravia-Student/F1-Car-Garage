using System.ComponentModel.DataAnnotations;

namespace F1_Car_Garage.ViewModels
{
    public class RacerViewModel
    {
        public int RacerId { get; set; }

        [Required, StringLength(100)]
        public string? Name { get; set; }

        [Required, StringLength(100)]
        public string? Nationality { get; set; }

        public int Points { get; set; }

        public int CarId { get; set; }

        [Required, StringLength(100)]
        public string? Username { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [StringLength(100, MinimumLength = 1)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
// The RacerViewModel class serves as a data transfer object that encapsulates the properties of a racer, including their name