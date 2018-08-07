using RemoteUpkeep.Models;
using RemoteUpkeep.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RemoteUpkeep.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "FirstName", ResourceType = typeof(Properties.Resources))]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName", ResourceType = typeof(Properties.Resources))]
        public string LastName { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Properties.Resources))]
        public string Email { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(Properties.Resources))]
        public string Phone { get; set; }

        [Required]
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
        [Display(Name = "UserType", ResourceType = typeof(Properties.Resources))]
        public UserType UserType { get; set; }
    }
}