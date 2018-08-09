using System.ComponentModel.DataAnnotations;

namespace RemoteUpkeep.ViewModels
{
    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email", ResourceType = typeof(Properties.Resources))]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email", ResourceType = typeof(Properties.Resources))]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Properties.Resources))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(Properties.Resources))]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(Properties.Resources))]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Properties.Resources))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Properties.Resources))]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(Properties.Resources))]
        public string Email { get; set; }
    }
}
