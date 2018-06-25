using System;
using System.Collections.Generic;
using RemoteUpkeep.Models;

namespace RemoteUpkeep.ViewModels
{
    public class OrderViewModel
    {
        public int? ServiceId { get; set; }

        public List<Service> Services { get; set; }

        public DateTime? Date { get; set; }
    }
}