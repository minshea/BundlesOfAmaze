using System.Diagnostics;
using System.Threading.Tasks;
using Discord;

namespace BundlesOfAmaze
{
    public class Logger
    {
        public async Task ClientOnLogAsync(LogMessage arg)
        {
            if (arg.Exception != null)
            {
                Debug.Write("");
            }

            await Task.Run(() => { });
        }
    }
}