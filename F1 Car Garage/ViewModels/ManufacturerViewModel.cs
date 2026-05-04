using System.ComponentModel.DataAnnotations;

namespace F1_Car_Garage.ViewModels // The ManufacturerViewModel class serves as a data transfer object for creating and editing manufacturer information, including properties for the manufacturer's name, country, tier, and associated user credentials for authentication and role management within the application. It includes validation attributes to ensure that the input data meets the required criteria before being processed by the controller actions.
{
    public class ManufacturerViewModel
    {
        public int ManufacturerId { get; set; }

        [Required, StringLength(100)]
        public string? Name { get; set; }

        [Required, StringLength(100)]
        public string? Country { get; set; }

        [Required]
        public string? Tier { get; set; }

        [Required, StringLength(100)]
        public string? Username { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required, StringLength(100, MinimumLength = 1)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}