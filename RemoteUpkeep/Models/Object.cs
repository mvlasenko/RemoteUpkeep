using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RemoteUpkeep.Models
{
    public class Object
    {
        public Object()
        {
            this.Actions = new List<Action>();
            this.Messages = new List<Message>();
        }

        [Key]
        public int Id { get; set; }

        public int ServiceId { get; set; }

        public virtual Service Service { get; set; }

        public virtual Location Location { get; set; }

        public virtual ICollection<Action> Actions { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}