using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using RemoteUpkeep.Validation;

namespace RemoteUpkeep.ViewModels
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public bool BrowserRemembered { get; set; }

        [Required]
        [Display(Name = "FirstName", ResourceType = typeof(Properties.Resources))]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName", ResourceType = typeof(Properties.Resources))]
        public string LastName { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(Properties.Resources))]
        public string Phone { get; set; }

        [UIHint("_Country")]
        [Display(Name = "Country", ResourceType = typeof(Properties.Resources))]
        public int? CountryId { get; set; }

        [UIHint("_Languages")]
        [Display(Name = "Languages", ResourceType = typeof(Properties.Resources))]
        [CheckboxListRequired]
        public ICollection<int> LanguageIds { get; set; }

        [UIHint("_Language")]
        [Display(Name = "PrimaryLanguage", ResourceType = typeof(Properties.Resources))]
        public int? PrimaryLanguageId { get; set; }

    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(Properties.Resources))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Properties.Resources))]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "CurrentPassword", ResourceType = typeof(Properties.Resources))]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(Properties.Resources))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Properties.Resources))]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}