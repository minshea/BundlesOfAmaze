using System.Threading.Tasks;

namespace BundlesOfAmaze.Application
{
    public interface IBackgroundService
    {
        Task HandleTickAsync();
    }
}