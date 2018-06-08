using System.ComponentModel.DataAnnotations;

namespace RemoteUpkeep.Models
{
    public class MessageAttachment
    {
        [Key]
        public int Id { get; set; }

        public string Text { get; set; }

        public string FilePath { get; set; }

        public int MessageId { get; set; }

        public virtual Message Message { get; set; }
    }
}