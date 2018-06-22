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
            this.Targets = new List<Target>();
        }

        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string CreatedByUserId { get; set; }

        [ScaffoldColumn(false)]
        public virtual ApplicationUser CreatedBy { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedDate { get; set; }

        //many-to-many
        public virtual ICollection<Target> Targets { get; set; }

    }
}