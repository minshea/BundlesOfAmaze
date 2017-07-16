using System.Threading.Tasks;
using BundlesOfAmaze.Data;

namespace BundlesOfAmaze.Application
{
    public interface IGiveCommandService
    {
        Task<ResultMessage> HandleAsync(Owner owner, string itemName);
    }
}