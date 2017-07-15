using System.Threading.Tasks;

namespace BundlesOfAmaze.Application
{
    public interface IHelpCommandService
    {
        Task<ResultMessage> HandleAsync(string subject);
    }
}