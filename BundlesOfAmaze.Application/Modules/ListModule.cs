using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.Configuration;

namespace BundlesOfAmaze.Application
{
    [Name("List")]
    public class ListModule : ModuleBase
    {
        private readonly IConfigurationRoot _configuration;

        public ListModule(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        [Command("list"), Alias("l")]
        [Summary("Lists possible activities. Use 'help list' for more information")]
        [Remarks("Usage: list")]
        public async Task HandleAsync()
        {
            var message = "\n**Lists**\n";
            message += "adventures - Available adventures\n";
            message += "jobs - Available jobs\n";

            await ReplyAsync(message);
        }

        [Command("list"), Alias("l")]
        [Summary("Lists possible activities. Use 'help list' for more information")]
        [Remarks("Usage: list [name]\nCommand to list activities or adventures your cat can embark on")]
        public async Task HandleAsync(string list)
        {
            var message = string.Empty;
            var embed = default(Embed);

            switch (list)
            {
                case Commands.ListAdventures:
                    message = "**Available adventures**\n\n";
                    message += "Usage: .amazecats go [adventure name]\n\n";
                    message += "- explore-neighbourhood\n";
                    break;

                case Commands.ListActivities:
                    message += "TODO: list of available activities\n";
                    break;

                default:
                    message = "\n**Lists**\n";
                    message += "adventures - Available adventures\n";
                    message += "jobs - Available jobs\n";
                    break;
            }

            await ReplyAsync(message, embed: embed);
        }

        [Command("version")]
        [Summary("Current application version")]
        [Remarks("Usage: version")]
        public async Task HandleVersionAsync()
        {
            await ReplyAsync("Version " + _configuration["BuildNumber"]);
        }
    }
}