using System.Threading.Tasks;
using Discord.WebSocket;

namespace BundlesOfAmaze.Application
{
    public interface ICommandService
    {
        Task<ResultMessage> Handle(SocketUserMessage msg);
    }
}