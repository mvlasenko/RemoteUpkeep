using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace RemoteUpkeep.Models
{
    public class Action
    {
        [Key]
        public int Id { get; set; }

        [UIHint("MultilineText")]
        [Display(Name = "Description", ResourceType = typeof(Properties.Resources))]
        public string Description { get; set; }

        [UIHint("_DatePicker")]
        [Display(Name = "DueDate", ResourceType = typeof(Properties.Resources))]
        public DateTime? DueDate { get; set; }

        public int OrderDetailsId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual OrderDetails OrderDetails { get; set; }

        [UIHint("_User")]
        [Display(Name = "AssignedUser", ResourceType = typeof(Properties.Resources))]
        public string AssignedUserId { get; set; }

        public virtual ApplicationUser AssignedUser { get; set; }

        [Display(Name = "ChangedBy", ResourceType = typeof(Properties.Resources))]
        public string ChangedByUserId { get; set; }

        public virtual ApplicationUser ChangedBy { get; set; }

        [Display(Name = "Changed", ResourceType = typeof(Properties.Resources))]
        public DateTime ChangedDateTime { get; set; }
    }
}