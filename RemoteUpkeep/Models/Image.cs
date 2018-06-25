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
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Text { get; set; }

        public string FileName { get; set; }

        public byte[] Binary { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<Target> Targets { get; set; }
    }
}