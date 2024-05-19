using System.ComponentModel.DataAnnotations;

namespace AroundTheWorld.ViewModels.IdentityModels
{
    public class TravelerRegistrationModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
