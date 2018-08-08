using RemoteUpkeep.Models;

namespace RemoteUpkeep.ViewModels
{
    public class EmailViewModel
    {
        public ApplicationUser Sender { get; set; }

        public ApplicationUser Receiver { get; set; }

        public string Body { get; set; }

        public string SignatureName { get; set; }
    }
}