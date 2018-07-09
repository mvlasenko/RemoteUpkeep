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
        public int TargetId { get; set; }

        public virtual Target Target { get; set; }

        [UIHint("_User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public OrderType OrderType { get; set; }

        //many-to-many
        public virtual ICollection<Service> Services { get; set; }
    }
}