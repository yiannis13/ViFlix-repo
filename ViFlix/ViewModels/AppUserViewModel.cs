using System.ComponentModel.DataAnnotations;

namespace ViFlix.ViewModels
{
    public class AppUserViewModel
    {
        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        public string ConfirmedPassword { get; set; }
    }
}