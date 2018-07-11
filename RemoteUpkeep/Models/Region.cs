using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RemoteUpkeep.Models
{
    public class Region
    {
        public Region()
        {
            this.Users = new List<ApplicationUser>();
            this.Orders = new List<Order>();
        }

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        [UIHint("_Country")]
        [Display(Name = "Country")]
        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; }

        public virtual ICollection<Order> Orders { get; }
    }
}