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
            this.Users2 = new List<ApplicationUser>();
            this.Messages = new List<Message>();
            this.Translations = new List<Translation>();
        }

        [Key]
        public int Id { get; set; }

        [Display(Name = "Title", ResourceType = typeof(Properties.Resources))]
        public string Title { get; set; }

        [Display(Name = "Code", ResourceType = typeof(Properties.Resources))]
        public string Code { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<ApplicationUser> Users { get; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<ApplicationUser> Users2 { get; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<Message> Messages { get; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<Translation> Translations { get; }
    }
}