using System;
using System.Collections.Generic;
using System.Text;

namespace BundlesOfAmaze.Shared
{
    public enum AccessLevel
    {
        Blocked,
        User,
        ServerMod,
        ServerAdmin,
        ServerOwner,
        BotOwner
    }
}