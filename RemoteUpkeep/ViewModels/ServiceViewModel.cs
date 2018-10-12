using System;
using System.ComponentModel.DataAnnotations;

namespace RemoteUpkeep.ViewModels
{
    public class ServiceViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Title", ResourceType = typeof(Properties.Resources))]
        public string Title { get; set; }

        [UIHint("MultilineText")]
        [Display(Name = "Description", ResourceType = typeof(Properties.Resources))]
        public string Description { get; set; }

        [Display(Name = "CreatedBy", ResourceType = typeof(Properties.Resources))]
        public string CreatedByUserId { get; set; }

        [Display(Name = "Created", ResourceType = typeof(Properties.Resources))]
        public DateTime CreatedDateTime { get; set; }
    }
}