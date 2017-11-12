using System.ComponentModel.DataAnnotations;

namespace ViFlix.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}