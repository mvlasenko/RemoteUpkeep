using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RemoteUpkeep.Models
{
    public class Image
    {
        public Image()
        {
            this.Messages = new List<Message>();
            this.Targets = new List<Target>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] Binary { get; set; }

        [UIHint("MultilineText")]
        public string Text { get; set; }

        [Display(Name = "Created")]
        public DateTime CreatedDateTime { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<Target> Targets { get; set; }
    }
}