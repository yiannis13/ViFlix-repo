using System.ComponentModel.DataAnnotations;

namespace Common.Models.ViewModels
{
    public class SignUpViewModel
    {
        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmedPassword { get; set; }
    }
}