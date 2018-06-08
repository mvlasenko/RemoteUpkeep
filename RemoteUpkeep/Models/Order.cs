using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RemoteUpkeep.Models
{
    public class Order
    {
        public Order()
        {
            this.Targets = new List<Target>();
        }

        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<Target> Targets { get; set; }
    }
}