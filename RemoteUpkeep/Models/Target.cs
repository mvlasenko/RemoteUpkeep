using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RemoteUpkeep.Models
{
    public class Target
    {
        public Target()
        {
            this.Services = new List<Service>();
            this.Images = new List<Image>();
            this.Actions = new List<Action>();
            this.Messages = new List<Message>();
        }

        [Key]
        public int Id { get; set; }

        public int LocationId { get; set; }

        public virtual Location Location { get; set; }

        //many-to-many
        public virtual ICollection<Service> Services { get; set; }

        //many-to-many
        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Action> Actions { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}