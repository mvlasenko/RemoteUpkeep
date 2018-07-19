﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

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

        public int OrderDetailsId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual OrderDetails OrderDetails { get; set; }

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