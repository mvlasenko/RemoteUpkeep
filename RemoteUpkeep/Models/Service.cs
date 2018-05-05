using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RemoteUpkeep.Models
{
    public class Service
    {
        public Service()
        {
            this.Objects = new List<Object>();
        }

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public virtual ApplicationUser CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Object> Objects { get; set; }

    }
}