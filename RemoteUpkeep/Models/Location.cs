using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        [UIHint("_Geography")]
        [NotMapped]
        [Display(Name = "Geography")]
        public string Geography {
            get
            {
                if (this.Latitude == 0 || this.Longitude == 0)
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

        public virtual ICollection<Target> Targets { get; set; }
    }
}