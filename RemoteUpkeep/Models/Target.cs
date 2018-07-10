using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RemoteUpkeep.Models
{
    public class Target
    {
        public Target()
        {
            this.Orders = new List<Order>();
            this.Images = new List<Image>();
            this.Actions = new List<Action>();
            this.Messages = new List<Message>();
        }

        [Key]
        public int Id { get; set; }

        [UIHint("_Region")]
        [Display(Name = "Region")]
        public int RegionId { get; set; }

        public virtual Region Region { get; set; }

        [UIHint("_Location")]
        [Display(Name = "Location")]
        public int? LocationId { get; set; }

        public virtual Location Location { get; set; }

        [UIHint("MultilineText")]
        public string Description { get; set; }

        [Display(Name = "Changed By")]
        public string ChangedByUserId { get; set; }

        public virtual ApplicationUser ChangedBy { get; set; }

        [Display(Name = "Changed")]
        public DateTime ChangedDateTime { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        //many-to-many
        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Action> Actions { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}