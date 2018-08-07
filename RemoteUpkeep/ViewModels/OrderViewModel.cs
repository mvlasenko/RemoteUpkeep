using System;
using System.ComponentModel.DataAnnotations;

namespace RemoteUpkeep.ViewModels
{
    public class OrderViewModel
    {
        //service info

        [UIHint("_Service")]
        [Display(Name = "Service", ResourceType = typeof(Properties.Resources))]
        public int? ServiceId { get; set; }

        //destination info

        [UIHint("_Region")]
        [Required]
        [Display(Name = "Region", ResourceType = typeof(Properties.Resources))]
        public int? RegionId { get; set; }

        [UIHint("_Geography")]
        [Display(Name = "Geography", ResourceType = typeof(Properties.Resources))]
        public string Geography { get; set; }

        [Display(Name = "HasGeography", ResourceType = typeof(Properties.Resources))]
        public bool HasGeography { get; set; }

        [Required]
        [UIHint("MultilineText")]
        [Display(Name = "Description", ResourceType = typeof(Properties.Resources))]
        public string Description { get; set; }

        [UIHint("_FileUpload")]
        [Display(Name = "Images", ResourceType = typeof(Properties.Resources))]
        public string FileIds { get; set; }

        [UIHint("_DatePicker")]
        [Display(Name = "DueDate", ResourceType = typeof(Properties.Resources))]
        public DateTime? DueDate { get; set; }
    }
}