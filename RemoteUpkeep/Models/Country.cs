using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RemoteUpkeep.Models
{
    public class Country
    {
        public Country()
        {
            this.Users = new List<ApplicationUser>();
            this.Regions = new List<Region>();
        }

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; }

        public virtual ICollection<Region> Regions { get; }
    }
}