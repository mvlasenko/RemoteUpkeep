using System;

namespace RemoteUpkeep.Models
{
    [Flags]
    public enum MessageType
    {
        None = 0,
        Email = 1,
        Viber = 2
    }

    [Flags]
    public enum UserType
    {
        None = 0,
        Admin = 1,
        LocalDealer = 2
    }
}