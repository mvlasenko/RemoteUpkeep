using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RemoteUpkeep.Models
{
    public class Order
    {
        public Order()
        {
            this.Services = new List<Service>();
        }

        [Key]
        public int Id { get; set; }

        [UIHint("_Target")]
        [Display(Name = "Target")]
        public int TargetId { get; set; }

        public virtual Target Target { get; set; }

        [UIHint("_User")]
        [Display(Name = "User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Display(Name = "Created")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "Type")]
        public OrderType OrderType { get; set; }

        //many-to-many
        public virtual ICollection<Service> Services { get; set; }
    }
}