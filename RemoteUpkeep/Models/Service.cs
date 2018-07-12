using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RemoteUpkeep.Models
{
    public class Service
    {
        public Service()
        {
            this.OrderDetails = new List<OrderDetails>();
        }

        [Key]
        public int Id { get; set; }

        [UIHint("MultilineText")]
        public string Description { get; set; }

        [Display(Name = "Created By")]
        public string CreatedByUserId { get; set; }

        public virtual ApplicationUser CreatedBy { get; set; }

        [Display(Name = "Created")]
        public DateTime CreatedDateTime { get; set; }

        //many-to-many
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

    }
}