using System;
using System.Linq;
using System.Threading.Tasks;
using BundlesOfAmaze.Data;
using Discord.WebSocket;

namespace BundlesOfAmaze.Application
{
    public class CommandService : ICommandService
    {
        private readonly ICatRepository _repository;
        private readonly ICreateCommandService _createCommandService;
        private readonly IHelpCommandService _helpCommandService;
        private readonly IGiveCommandService _giveCommandService;
        private readonly IListCommandService _listCommandService;

        public CommandService(
            ICatRepository repository,
            ICreateCommandService createCommandService,
            IHelpCommandService helpCommandService,
            IGiveCommandService giveCommandService,
            IListCommandService listCommandService)
        {
            _repository = repository;
            _createCommandService = createCommandService;
            _helpCommandService = helpCommandService;
            _giveCommandService = giveCommandService;
            _listCommandService = listCommandService;
        }

        public async Task<ResultMessage> HandleAsync(SocketUserMessage msg)
        {
            var cleanCommand = msg.Content.Trim();
            var commandParts = cleanCommand.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            if (commandParts.ElementAtOrDefault(0) != Commands.Prefix)
            {
                return null;
            }

            var ownerId = msg.Author.Id.ToString();

            switch (commandParts.ElementAtOrDefault(1))
            {
                case Commands.Create:
                    return await _createCommandService.HandleAsync(ownerId, commandParts.ElementAtOrDefault(2), commandParts.ElementAtOrDefault(3));

                case Commands.Give:
                    return await _giveCommandService.HandleAsync(ownerId, commandParts.ElementAtOrDefault(2));

                case Commands.List:
                    return await _listCommandService.HandleAsync(ownerId, commandParts.ElementAtOrDefault(2));

                case Commands.Help:
                    return await _helpCommandService.HandleAsync(commandParts.ElementAtOrDefault(2));

                default:
                    var cat1 = await _repository.FindByOwnerAsync(ownerId);
                    if (cat1 == null)
                    {
                        return new ResultMessage("You don't have a cat yet!\nCreate on using the 'create' command.");
                    }

                    return new ResultMessage(CatSheet.GetSheet(cat1));
            }
        }

        ////private async Task<ResultMessage> HandlePoke(Cat cat)
        ////{
        ////    ////.amazecats poke

        ////    var result = $"{cat.Name} flops over\n";

        ////    return new ResultMessage(result);
        ////}
    }
}