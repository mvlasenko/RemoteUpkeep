using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RemoteUpkeep.Models
{
    public class Service
    {
        public Service()
        {
            this.Orders = new List<Order>();
        }

        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Description { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string CreatedByUserId { get; set; }

        public virtual ApplicationUser CreatedBy { get; set; }

        public DateTime CreatedDateTime { get; set; }

        //many-to-many
        public virtual ICollection<Order> Orders { get; set; }

    }
}