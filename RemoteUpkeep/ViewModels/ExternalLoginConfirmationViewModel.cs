using System.ComponentModel.DataAnnotations;

namespace RemoteUpkeep.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Country")]
        [UIHint("_Country")]
        public string CountryId { get; set; }

        [Display(Name = "Languages")]
        [UIHint("_Languages")]
        public string Languages { get; set; }
    }
}