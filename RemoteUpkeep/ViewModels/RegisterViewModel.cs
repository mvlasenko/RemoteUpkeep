using RemoteUpkeep.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RemoteUpkeep.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "FirstName", ResourceType = typeof(Properties.Resources))]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName", ResourceType = typeof(Properties.Resources))]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email", ResourceType = typeof(Properties.Resources))]
        public string Email { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(Properties.Resources))]
        public string Phone { get; set; }

        [Display(Name = "Country", ResourceType = typeof(Properties.Resources))]
        [UIHint("_Country")]
        public int? CountryId { get; set; }

        [Display(Name = "Region", ResourceType = typeof(Properties.Resources))]
        [UIHint("_Region")]
        public int? RegionId { get; set; }

        [Display(Name = "Languages", ResourceType = typeof(Properties.Resources))]
        [UIHint("_Languages")]
        [CheckboxListRequired]
        public ICollection<int> LanguageIds { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Properties.Resources))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Properties.Resources))]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}