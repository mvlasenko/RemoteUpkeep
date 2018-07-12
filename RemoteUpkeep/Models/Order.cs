using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RemoteUpkeep.Models
{
    public class Order
    {
        public Order()
        {
            this.OrderDetails = new List<OrderDetails>();
        }

        [Key]
        public int Id { get; set; }

        [UIHint("_User")]
        [Display(Name = "User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Display(Name = "Created")]
        public DateTime CreatedDateTime { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}