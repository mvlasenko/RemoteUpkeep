using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RemoteUpkeep.Models
{
    public class Message
    {
        public Message()
        {
            this.Attachments = new List<Image>();
            this.Targets = new List<Target>();
        }

        [Key]
        public int Id { get; set; }

        public string SenderId { get; set; }

        public virtual ApplicationUser Sender { get; set; }

        public string ReceiverId { get; set; }

        public virtual ApplicationUser Receiver { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public MessageType MessageType { get; set; }

        public virtual ICollection<Image> Attachments { get; set; }

        public virtual ICollection<Target> Targets { get; set; }
    }
}