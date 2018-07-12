using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RemoteUpkeep.Models
{
    public class Country
    {
        public Country()
        {
            this.Users = new List<ApplicationUser>();
        }

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; }
    }
}