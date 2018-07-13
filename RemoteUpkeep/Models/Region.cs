using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RemoteUpkeep.Models
{
    public class Region
    {
        public Region()
        {
            this.Users = new List<ApplicationUser>();
            this.Targets = new List<Target>();
        }

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        [UIHint("_Country")]
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

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

        public virtual ICollection<ApplicationUser> Users { get; }

        public virtual ICollection<Target> Targets { get; }
    }
}