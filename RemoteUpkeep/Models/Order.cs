using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RemoteUpkeep.Models
{
    public class Order
    {
        public Order()
        {
            this.Images = new List<Image>();
            this.Actions = new List<Action>();
            this.Messages = new List<Message>();
            this.Services = new List<Service>();
        }

        [Key]
        public int Id { get; set; }

        [UIHint("_Region")]
        [Display(Name = "Region")]
        public int RegionId { get; set; }

        public virtual Region Region { get; set; }

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

        [UIHint("MultilineText")]
        public string Description { get; set; }

        [UIHint("_User")]
        [Display(Name = "User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Display(Name = "Created")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "Type")]
        public OrderType OrderType { get; set; }

        [Display(Name = "Changed By")]
        public string ChangedByUserId { get; set; }

        public virtual ApplicationUser ChangedBy { get; set; }

        [Display(Name = "Changed")]
        public DateTime? ChangedDateTime { get; set; }

        //many-to-many
        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Action> Actions { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}