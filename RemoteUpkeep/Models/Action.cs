using System;
using System.ComponentModel.DataAnnotations;

namespace RemoteUpkeep.Models
{
    public class Action
    {
        [Key]
        public int Id { get; set; }

        [UIHint("MultilineText")]
        public string Description { get; set; }

        [UIHint("_DatePicker")]
        public DateTime? DueDate { get; set; }

        [UIHint("_Target")]
        public int TargetId { get; set; }

        public virtual Target Target { get; set; }

        [UIHint("_User")]
        public string AssignedUserId { get; set; }

        public virtual ApplicationUser AssignedUser { get; set; }

        public string ChangedByUserId { get; set; }

        public virtual ApplicationUser ChangedBy { get; set; }

        public DateTime ChangedDateTime { get; set; }
    }
}