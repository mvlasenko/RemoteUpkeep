using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RemoteUpkeep.Models
{
    public class OrderDetails
    {
        public OrderDetails()
        {
            this.Actions = new List<Action>();
            this.Messages = new List<Message>();
            this.Services = new List<Service>();
        }

        [Key]
        public int Id { get; set; }

        [UIHint("_Target")]
        [Display(Name = "Target")]
        public int TargetId { get; set; }

        public virtual Target Target { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        [Display(Name = "Status")]
        public OrderStatus OrderStatus { get; set; }

        public virtual ICollection<Action> Actions { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        //many-to-many
        public virtual ICollection<Service> Services { get; set; }
    }
}