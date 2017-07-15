using System.Threading.Tasks;
using BundlesOfAmaze.Application;
using Discord.Commands;
using Discord.WebSocket;

namespace BundlesOfAmaze
{
    public class MessageHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly ICommandService _commandService;

        public MessageHandler(ICommandService commandService, DiscordSocketClient client)
        {
            _client = client;
            _commandService = commandService;
        }

        public async Task HandleMessageAsync(SocketMessage arg)
        {
            var msg = arg as SocketUserMessage;

            // Check if the received message is from a user
            if (msg == null)
            {
                return;
            }

            var result = await _commandService.HandleAsync(msg);
            if (result == null)
            {
                return;
            }

            // Create a new command context
            var context = new SocketCommandContext(_client, msg);
            await context.Channel.SendMessageAsync(result.Message, embed: result.Embed);
        }
    }
}