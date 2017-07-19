using System;
using System.Linq;
using System.Threading.Tasks;
using BundlesOfAmaze.Data;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace BundlesOfAmaze.Application
{
    public class CommandService : ICommandService
    {
        private readonly IConfigurationRoot _configuration;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICatRepository _catRepository;
        private readonly ICreateCommandService _createCommandService;
        private readonly IHelpCommandService _helpCommandService;
        private readonly IGiveCommandService _giveCommandService;
        private readonly IListCommandService _listCommandService;
        private readonly IGoCommandService _goCommandService;

        public CommandService(
            IConfigurationRoot configuration,
            IOwnerRepository ownerRepository,
            ICatRepository catRepository,
            ICreateCommandService createCommandService,
            IHelpCommandService helpCommandService,
            IGiveCommandService giveCommandService,
            IListCommandService listCommandService,
            IGoCommandService goCommandService)
        {
            _configuration = configuration;
            _ownerRepository = ownerRepository;
            _catRepository = catRepository;
            _createCommandService = createCommandService;
            _helpCommandService = helpCommandService;
            _giveCommandService = giveCommandService;
            _listCommandService = listCommandService;
            _goCommandService = goCommandService;
        }

        public async Task<ResultMessage> HandleAsync(SocketUserMessage msg)
        {
            var cleanCommand = msg.Content.Trim();
            var commandParts = cleanCommand.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var prefix = _configuration["NETCORE_ENVIRONMENT"] == "production" ? Commands.Prefix : Commands.PrefixDev;
            if (commandParts.ElementAtOrDefault(0) != prefix)
            {
                return null;
            }

            var authorId = msg.Author.Id.ToString();
            var owner = await _ownerRepository.FindByAuthorIdAsync(authorId);

            if (owner == null)
            {
                // There is no owner yet. Check for the create command and generate one
                switch (commandParts.ElementAtOrDefault(1))
                {
                    case Commands.Create:
                        owner = await _createCommandService.CreateOwnerAsync(msg);
                        break;

                    default:
                        return new ResultMessage("You don't have a cat yet!\nCreate one using the 'create' command.");
                }
            }

            switch (commandParts.ElementAtOrDefault(1))
            {
                case Commands.Create:
                    return await _createCommandService.HandleAsync(owner, commandParts.ElementAtOrDefault(2), commandParts.ElementAtOrDefault(3));

                case Commands.Give:
                    return await _giveCommandService.HandleAsync(owner, commandParts.ElementAtOrDefault(2));

                case Commands.List:
                    return await _listCommandService.HandleAsync(owner, commandParts.ElementAtOrDefault(2));

                case Commands.Go:
                    return await _goCommandService.HandleAsync(owner, commandParts.ElementAtOrDefault(2));

                case Commands.Help:
                    return await _helpCommandService.HandleAsync(commandParts.ElementAtOrDefault(2));

                default:
                    var cat1 = await _catRepository.FindByOwnerAsync(owner.Id);
                    if (cat1 == null)
                    {
                        return new ResultMessage(
                            "You don't have a cat yet!\nCreate one using the 'create' command.");
                    }

                    return new ResultMessage(CatSheet.GetSheet(cat1));
            }
        }
    }
}