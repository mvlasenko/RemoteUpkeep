using System;
using System.ComponentModel.DataAnnotations;

namespace RemoteUpkeep.Models
{
    public class Action
    {
        [Key]
        public int Id { get; set; }

        public DateTime DueDate { get; set; }

        public int TargetId { get; set; }

        public virtual Target Target { get; set; }
    }
}