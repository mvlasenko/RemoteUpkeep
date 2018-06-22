using System;
using System.Collections.Generic;

namespace RemoteUpkeep.Models
{
    public class OrderViewModel
    {
        public int? ServiceId { get; set; }

        public List<Service> Services { get; set; }

        public DateTime? Date { get; set; }
    }
}