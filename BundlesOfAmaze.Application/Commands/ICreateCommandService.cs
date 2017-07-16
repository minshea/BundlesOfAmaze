using System.Threading.Tasks;
using BundlesOfAmaze.Data;

namespace BundlesOfAmaze.Application
{
    public interface ICreateCommandService
    {
        Task<ResultMessage> HandleAsync(Owner owner, string rawName, string rawGender);
    }
}