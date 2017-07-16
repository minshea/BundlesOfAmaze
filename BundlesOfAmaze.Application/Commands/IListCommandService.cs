using System.Threading.Tasks;
using BundlesOfAmaze.Data;

namespace BundlesOfAmaze.Application
{
    public interface IListCommandService
    {
        Task<ResultMessage> HandleAsync(Owner owner, string command);
    }
}