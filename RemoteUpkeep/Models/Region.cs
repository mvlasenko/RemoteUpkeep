using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Title { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; }

        public virtual ICollection<Target> Targets { get; }
    }
}