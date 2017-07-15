using System.Threading.Tasks;

namespace BundlesOfAmaze.Application
{
    public interface IGiveCommandService
    {
        Task<ResultMessage> HandleAsync(string ownerId, string item);
    }
}