using Discord;

namespace BundlesOfAmaze.Application
{
    public class ResultMessage
    {
        public string Message { get; }
        public Embed Embed { get; }

        public ResultMessage(string message)
        {
            Message = message;
        }

        public ResultMessage(Embed embed)
        {
            Message = string.Empty;
            Embed = embed;
        }

        public ResultMessage(string message, Embed embed)
        {
            Message = message;
            Embed = embed;
        }
    }
}