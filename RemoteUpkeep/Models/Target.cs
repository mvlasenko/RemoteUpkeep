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

        public int RegionId { get; set; }

        public virtual Region Region { get; set; }

        public int? LocationId { get; set; }

        public virtual Location Location { get; set; }

        public string Description { get; set; }

        public string ChangedByUserId { get; set; }

        public virtual ApplicationUser ChangedBy { get; set; }

        public DateTime ChangedDateTime { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        //many-to-many
        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Action> Actions { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}