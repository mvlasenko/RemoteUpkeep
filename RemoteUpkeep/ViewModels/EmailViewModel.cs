using RemoteUpkeep.Models;

namespace RemoteUpkeep.ViewModels
{
    public class EmailViewModel
    {
        public ApplicationUser Sender { get; set; }

        public ApplicationUser Receiver { get; set; }
    }
}