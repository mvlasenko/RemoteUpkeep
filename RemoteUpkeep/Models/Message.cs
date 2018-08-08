using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace RemoteUpkeep.Models
{
    public class Message
    {
        public Message()
        {
            this.Attachments = new List<Image>();
        }

        [Key]
        public int Id { get; set; }

        [UIHint("_User")]
        [Display(Name = "Sender", ResourceType = typeof(Properties.Resources))]
        public string SenderId { get; set; }

        public virtual ApplicationUser Sender { get; set; }

        [UIHint("_User")]
        [Display(Name = "Receiver", ResourceType = typeof(Properties.Resources))]
        public string ReceiverId { get; set; }

        public virtual ApplicationUser Receiver { get; set; }

        [Required]
        [Display(Name = "Subject", ResourceType = typeof(Properties.Resources))]
        public string Subject { get; set; }

        [Required]
        [UIHint("MultilineText")]
        [Display(Name = "Text", ResourceType = typeof(Properties.Resources))]
        public string Text { get; set; }

        [Display(Name = "Date", ResourceType = typeof(Properties.Resources))]
        public DateTime Date { get; set; }

        [Display(Name = "MessageType", ResourceType = typeof(Properties.Resources))]
        [ScriptIgnore(ApplyToOverrides = true)]
        public MessageType MessageType { get; set; }

        public string MessageTypeName
        {
            get
            {
                return this.MessageType.ToString();
            }
        }

        [Display(Name = "Order", ResourceType = typeof(Properties.Resources))]
        public int? OrderDetailsId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual OrderDetails OrderDetails { get; set; }

        //many-to-many
        public virtual ICollection<Image> Attachments { get; set; }
    }
}