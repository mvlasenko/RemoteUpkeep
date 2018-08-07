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

        [Display(Name = "Title", ResourceType = typeof(Properties.Resources))]
        public string Title { get; set; }

        [UIHint("MultilineText")]
        [Display(Name = "Description", ResourceType = typeof(Properties.Resources))]
        public string Description { get; set; }

        [Display(Name = "CreatedBy", ResourceType = typeof(Properties.Resources))]
        public string CreatedByUserId { get; set; }

        public virtual ApplicationUser CreatedBy { get; set; }

        [Display(Name = "Created", ResourceType = typeof(Properties.Resources))]
        public DateTime CreatedDateTime { get; set; }

        //many-to-many
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

    }
}