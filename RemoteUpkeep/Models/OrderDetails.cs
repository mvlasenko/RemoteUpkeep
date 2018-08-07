using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

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
        [Display(Name = "Target", ResourceType = typeof(Properties.Resources))]
        public int TargetId { get; set; }

        public virtual Target Target { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        [Display(Name = "OrderStatus", ResourceType = typeof(Properties.Resources))]
        [ScriptIgnore(ApplyToOverrides = true)]
        public OrderStatus OrderStatus { get; set; }

        public string OrderStatusName
        {
            get
            {
                return this.OrderStatus.ToString();
            }
        }

        public virtual ICollection<Action> Actions { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        //many-to-many
        public virtual ICollection<Service> Services { get; set; }
        
        [UIHint("_Services")]
        [Display(Name = "Services", ResourceType = typeof(Properties.Resources))]
        public List<int> ServiceIds { get; set; }
    }
}