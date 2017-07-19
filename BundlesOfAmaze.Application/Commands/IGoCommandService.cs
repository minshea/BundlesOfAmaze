using System.Threading.Tasks;
using BundlesOfAmaze.Data;

namespace BundlesOfAmaze.Application
{
    public interface IGoCommandService
    {
        Task<ResultMessage> HandleAsync(Owner owner, string rawDestinationName);
    }
}