using System.Diagnostics;
using System.Threading.Tasks;
using Discord;

namespace BundlesOfAmaze
{
    public class Logger
    {
        public async Task ClientOnLogAsync(LogMessage arg)
        {
            // TODO: implement some form of logging

            if (arg.Exception != null)
            {
                Debug.Write("");
            }

            await Task.Run(() => { });
        }
    }
}