using System.Threading.Tasks;
using BundlesOfAmaze.Data;
using Discord;

namespace BundlesOfAmaze.Application
{
    public interface IOwnerService
    {
        Task Initialize(IUser user);

        Task SetCurrentOwner(Owner owner);
    }
}