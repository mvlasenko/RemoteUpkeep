using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace RemoteUpkeep.Models
{
    public class Language
    {
        public Language()
        {
            this.Users = new List<ApplicationUser>();
        }

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<ApplicationUser> Users { get; }
    }
}