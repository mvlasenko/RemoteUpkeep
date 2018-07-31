using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

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

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<ApplicationUser> Users { get; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<Region> Regions { get; }
    }
}