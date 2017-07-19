using System.Threading.Tasks;
using BundlesOfAmaze.Data;
using Discord.WebSocket;

namespace BundlesOfAmaze.Application
{
    public interface ICreateCommandService
    {
        Task<ResultMessage> HandleAsync(Owner owner, string rawName, string rawGender);

        Task<Owner> CreateOwnerAsync(SocketUserMessage msg);
    }
}