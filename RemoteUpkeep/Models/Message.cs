using System;

namespace RemoteUpkeep.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public ApplicationUser User { get; set; }
    }
}