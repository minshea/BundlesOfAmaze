using System.Threading.Tasks;

namespace BundlesOfAmaze.Application
{
    public interface ICreateCommandService
    {
        Task<ResultMessage> HandleAsync(long ownerId, string rawName, string rawGender);
    }
}