using System.Threading.Tasks;
using BundlesOfAmaze.Data;
using Discord;

namespace BundlesOfAmaze.Application
{
    public class ListCommandService : IListCommandService
    {
        private readonly ICatRepository _repository;

        public ListCommandService(ICatRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultMessage> HandleAsync(long ownerId, string commandPart)
        {
            ////.amazecats list
            ////.amazecats list adventures

            var message = string.Empty;
            var embed = default(Embed);

            switch (commandPart)
            {
                case Commands.ListAdventures:
                    message += "TODO: list of available adventures\n";
                    break;

                case Commands.ListActivities:
                    message += "TODO: list of available activities\n";
                    break;

                case Commands.ListCats:

                    var cats = await _repository.FindAllAsync();
                    var embedBuilder = new EmbedBuilder();
                    foreach (var cat in cats)
                    {
                    }

                    embed = embedBuilder.Build();
                    break;

                default:
                    message = "\n**Lists**\n";
                    message += "adventures - Available adventures\n";
                    message += "jobs - Available jobs\n";
                    break;
            }

            return new ResultMessage(message, embed);
        }
    }
}