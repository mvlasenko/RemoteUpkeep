using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;

namespace RemoteUpkeep.Models
{
    public class Location
    {
        public Location()
        {
            this.Targets = new List<Target>();
        }

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        [UIHint("MultilineText")]
        public string Description { get; set; }

        [UIHint("_Geography")]
        public DbGeography Geography { get; set; }

        [Display(Name = "Changed By")]
        public string ChangedByUserId { get; set; }

        public virtual ApplicationUser ChangedBy { get; set; }

        [Display(Name = "Changed")]
        public DateTime ChangedDateTime { get; set; }

        public virtual ICollection<Target> Targets { get; set; }
    }
}