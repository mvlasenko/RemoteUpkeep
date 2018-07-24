using System;
using System.ComponentModel.DataAnnotations;

namespace RemoteUpkeep.ViewModels
{
    public class OrderViewModel
    {
        //service info

        [Display(Name = "Service")]
        [UIHint("_Service")]
        public int? ServiceId { get; set; }

        //destination info

        [Required]
        [Display(Name = "Region")]
        [UIHint("_Region")]
        public int? RegionId { get; set; }

        [UIHint("_Geography")]
        [Display(Name = "Geography")]
        public string Geography { get; set; }

        [Display(Name = "I know exact location of the grave")]
        public bool HasGeography { get; set; }

        [Required]
        [UIHint("MultilineText")]
        public string Description { get; set; }

        [Display(Name = "Images")]
        [UIHint("_FileUpload")]
        public string FileIds { get; set; }

        [Display(Name = "Due Date")]
        [UIHint("_DatePicker")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DueDate { get; set; }
    }
}