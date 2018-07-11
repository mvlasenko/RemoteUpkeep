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
            this.Orders = new List<Order>();
        }

        [Key]
        public int Id { get; set; }

        [UIHint("_User")]
        [Display(Name = "Sender")]
        public string SenderId { get; set; }

        public virtual ApplicationUser Sender { get; set; }

        [UIHint("_User")]
        [Display(Name = "Receiver")]
        public string ReceiverId { get; set; }

        public virtual ApplicationUser Receiver { get; set; }

        [UIHint("MultilineText")]
        public string Text { get; set; }

        public DateTime Date { get; set; }

        [Display(Name = "Type")]
        public MessageType MessageType { get; set; }

        public virtual ICollection<Image> Attachments { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}