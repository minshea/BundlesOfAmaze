using System;
using System.Threading.Tasks;
using Discord;

namespace BundlesOfAmaze
{
    public class Logger
    {
        public Task ClientOnLogAsync(LogMessage arg)
        {
            // TODO: implement some form of logging

            Console.WriteLine(arg);

            return Task.FromResult(0);
        }
    }
}