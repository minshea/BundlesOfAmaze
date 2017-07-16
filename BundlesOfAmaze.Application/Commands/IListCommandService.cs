using System.Threading.Tasks;

namespace BundlesOfAmaze.Application
{
    public interface IListCommandService
    {
        Task<ResultMessage> HandleAsync(long ownerId, string command);
    }
}