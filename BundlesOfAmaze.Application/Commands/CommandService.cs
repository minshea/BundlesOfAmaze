using System;
using System.Linq;
using System.Threading.Tasks;
using BundlesOfAmaze.Data;
using Discord.WebSocket;

namespace BundlesOfAmaze.Application
{
    public class CommandService : ICommandService
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICatRepository _catRepository;
        private readonly ICreateCommandService _createCommandService;
        private readonly IHelpCommandService _helpCommandService;
        private readonly IGiveCommandService _giveCommandService;
        private readonly IListCommandService _listCommandService;

        public CommandService(
            IOwnerRepository ownerRepository,
            ICatRepository catRepository,
            ICreateCommandService createCommandService,
            IHelpCommandService helpCommandService,
            IGiveCommandService giveCommandService,
            IListCommandService listCommandService)
        {
            _ownerRepository = ownerRepository;
            _catRepository = catRepository;
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

            var authorId = msg.Author.Id.ToString();
            var owner = await _ownerRepository.FindByAuthorIdAsync(authorId);

            if (owner == null)
            {
                // There is no owner yet. Check for the create command and generate one
                switch (commandParts.ElementAtOrDefault(1))
                {
                    case Commands.Create:
                        owner = new Owner(msg.Author.Id.ToString(), msg.Author.Username);
                        await _ownerRepository.AddAsync(owner);
                        await _ownerRepository.SaveChangesAsync();
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