using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

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
        [Display(Name = "Region", ResourceType = typeof(Properties.Resources))]
        public int RegionId { get; set; }

        public virtual Region Region { get; set; }

        [Display(Name = "Title", ResourceType = typeof(Properties.Resources))]
        public string Title { get; set; }

        [UIHint("MultilineText")]
        [Display(Name = "Description", ResourceType = typeof(Properties.Resources))]
        public string Description { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        [UIHint("_Geography")]
        [NotMapped]
        [Display(Name = "Geography", ResourceType = typeof(Properties.Resources))]
        public string Geography
        {
            get
            {
                if (this.Latitude == 0 || this.Longitude == 0 || this.Latitude == null || this.Longitude == null)
                    return null;
                return string.Format(new CultureInfo("en-US"), "({0}, {1})", this.Latitude, this.Longitude);
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.Latitude = double.Parse(value.Split(',')[0].Trim('(', ')', ' '), new CultureInfo("en-US"));
                    this.Longitude = double.Parse(value.Split(',')[1].Trim('(', ')', ' '), new CultureInfo("en-US"));
                }
            }
        }

        [Display(Name = "ChangedBy", ResourceType = typeof(Properties.Resources))]
        public string ChangedByUserId { get; set; }

        public virtual ApplicationUser ChangedBy { get; set; }

        [Display(Name = "Changed", ResourceType = typeof(Properties.Resources))]
        public DateTime ChangedDateTime { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        //many-to-many
        public virtual ICollection<Image> Images { get; set; }

        //view only
        [NotMapped]
        public int OrderId { get; set; }

        //view only
        [Display(Name = "Images", ResourceType = typeof(Properties.Resources))]
        [UIHint("_FileUpload")]
        [NotMapped]
        public string FileIds { get; set; }

        //view only
        [Display(Name = "Images", ResourceType = typeof(Properties.Resources))]
        [UIHint("_Image")]
        [NotMapped]
        public string ImageIds { get; set; }
    }
}