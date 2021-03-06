﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;

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

        public string Text { get; set; }

        public DateTime CreatedDateTime { get; set; }

        //many-to-many
        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<Message> Messages { get; set; }

        //many-to-many
        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<Target> Targets { get; set; }
    }
}