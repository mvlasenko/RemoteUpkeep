using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
        [ScriptIgnore(ApplyToOverrides = true)]
        public MessageType MessageType { get; set; }

        public string MessageTypeName
        {
            get
            {
                return this.MessageType.ToString();
            }
        }

        [Display(Name = "Order")]
        public int? OrderDetailsId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual OrderDetails OrderDetails { get; set; }

        //many-to-many
        public virtual ICollection<Image> Attachments { get; set; }
    }
}