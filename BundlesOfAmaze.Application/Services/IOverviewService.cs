using System.Threading.Tasks;
using BundlesOfAmaze.Data;

namespace BundlesOfAmaze.Application
{
    public interface IOverviewService
    {
        Task<ResultMessage> GetOverviewAsync(Cat cat);
    }
}