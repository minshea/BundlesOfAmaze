using BundlesOfAmaze.Data;
using Discord;

namespace BundlesOfAmaze.Application
{
    public interface ICurrentOwner
    {
        ////IUser User { get; }

        Owner Owner { get; }

        Cat Cat { get; }
    }
}