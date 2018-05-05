using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RemoteUpkeep.Models
{
    public class Message
    {
        public Message()
        {
            this.Attachments = new List<MessageAttachment>();
        }

        [Key]
        public int Id { get; set; }

        public int SenderId { get; set; }

        public virtual ApplicationUser Sender { get; set; }

        public int ReceiverId { get; set; }

        public virtual ApplicationUser Receiver { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public int? ObjectId { get; set; }

        public virtual Object Object { get; set; }

        public MessageTarget MessageTarget { get; set; }

        public virtual ICollection<MessageAttachment> Attachments { get; set; }
    }
}