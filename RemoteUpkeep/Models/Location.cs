using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;

namespace RemoteUpkeep.Models
{
    public class Location
    {
        public Location()
        {
            this.Objects = new List<Object>();
        }

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DbGeography Geography { get; set; }

        public virtual ICollection<Object> Objects { get; set; }
    }
}