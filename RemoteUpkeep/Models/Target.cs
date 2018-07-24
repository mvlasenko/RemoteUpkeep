using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RemoteUpkeep.Models
{
    public class Target
    {
        public Target()
        {
            this.Images = new List<Image>();
            this.OrderDetails = new List<OrderDetails>();
        }

        [Key]
        public int Id { get; set; }

        [UIHint("_Region")]
        [Display(Name = "Region")]
        public int RegionId { get; set; }

        public virtual Region Region { get; set; }

        public string Title { get; set; }

        [UIHint("MultilineText")]
        public string Description { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        [UIHint("_Geography")]
        [NotMapped]
        [Display(Name = "Geography")]
        public string Geography
        {
            get
            {
                if (this.Latitude == 0 || this.Longitude == 0 || this.Latitude == null || this.Longitude == null)
                    return null;
                return string.Format("({0}, {1})", this.Latitude, this.Longitude);
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.Latitude = double.Parse(value.Split(',')[0].Trim('(', ')', ' '));
                    this.Longitude = double.Parse(value.Split(',')[1].Trim('(', ')', ' '));
                }
            }
        }

        [Display(Name = "Changed By")]
        public string ChangedByUserId { get; set; }

        public virtual ApplicationUser ChangedBy { get; set; }

        [Display(Name = "Changed")]
        public DateTime ChangedDateTime { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        //many-to-many
        public virtual ICollection<Image> Images { get; set; }

        //view only
        [NotMapped]
        public int OrderId { get; set; }

        //view only
        [Display(Name = "Images")]
        [UIHint("_FileUpload")]
        [NotMapped]
        public string FileIds { get; set; }

        //view only
        [Display(Name = "Images")]
        [UIHint("_Image")]
        [NotMapped]
        public string ImageIds { get; set; }
    }
}