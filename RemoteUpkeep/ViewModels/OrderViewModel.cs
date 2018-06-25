using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace RemoteUpkeep.ViewModels
{
    public class OrderViewModel
    {
        //service info

        [Display(Name = "Service")]
        [UIHint("_Service")]
        public int? ServiceId { get; set; }

        //destination info

        [Display(Name = "Region")]
        [UIHint("_Region")]
        public int? RegionId { get; set; }

        [Required]
        [UIHint("MultilineText")]
        public string Description { get; set; }

        [Display(Name = "Images")]
        [UIHint("_FileUpload")]
        public List<Image> Images { get; set; }

        public DateTime? DueDate { get; set; }
    }
}