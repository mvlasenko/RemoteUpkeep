using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RemoteUpkeep.Models
{
    public class Order
    {
        public Order()
        {
            this.Objects = new List<Object>();
        }

        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<Object> Objects { get; set; }
    }
}