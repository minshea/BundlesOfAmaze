using System.Threading.Tasks;

namespace BundlesOfAmaze.Application
{
    public interface ICreateCommandService
    {
        Task<ResultMessage> HandleAsync(string ownerId, string rawName, string rawGender);
    }
}