using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class RegistrationModel
    {
        // Existing properties
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }

        public IList<AuthenticationScheme>? ExternalLogins { get; set; }

        // New fields
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Department")]
        public string Department { get; set; }

        [Required]
        [Display(Name = "Designation")]
        public string Designation { get; set; }


        [Required]
        [Display(Name = "Pin")]
        public string Pin { get; set; }
    }
}
