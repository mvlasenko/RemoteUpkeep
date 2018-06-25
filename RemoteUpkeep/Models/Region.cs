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
        }

        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Title { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; }
    }
}