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
        [Display(Name = "Due Date")]
        public DateTime? DueDate { get; set; }

        [UIHint("_Order")]
        [Display(Name = "Order")]
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        [UIHint("_User")]
        [Display(Name = "Assigned User")]
        public string AssignedUserId { get; set; }

        public virtual ApplicationUser AssignedUser { get; set; }

        [Display(Name = "Changed By")]
        public string ChangedByUserId { get; set; }

        public virtual ApplicationUser ChangedBy { get; set; }

        [Display(Name = "Changed")]
        public DateTime ChangedDateTime { get; set; }
    }
}