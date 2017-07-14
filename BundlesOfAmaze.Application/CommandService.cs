using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BundlesOfAmaze.Data;
using Discord;
using Discord.WebSocket;

namespace BundlesOfAmaze.Application
{
    public class CommandService : ICommandService
    {
        private readonly ICatRepository _repository;

        public CommandService(ICatRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultMessage> Handle(SocketUserMessage msg)
        {
            var cleanCommand = msg.Content.Trim();
            var commandParts = cleanCommand.Split(' ').ToList();
            var count = commandParts.Count;

            if (commandParts[0] != Commands.Prefix)
            {
                return null;
            }

            var ownerId = msg.Author.Id.ToString();

            if (commandParts[1] == Commands.Create)
            {
                return await HandleCreate(ownerId, commandParts.GetRange(2, count - 2));
            }

            var cat = await _repository.FindByOwnerAsync(ownerId);
            if (cat == null)
            {
                return null;
            }

            switch (commandParts[1])
            {
                case Commands.Feed:
                    return await HandleFeed(cat, commandParts.GetRange(2, count - 2));

                case Commands.Poke:
                    return await HandlePoke(cat);

                case Commands.List:
                    return await HandleList(cat, commandParts.GetRange(2, count - 2));

                case Commands.Help:
                    return HandleHelp(commandParts.Count > 2 ? commandParts[2] : null);

                default:
                    return null;
            }
        }

        private async Task<ResultMessage> HandleList(Cat cat, IList<string> commandParts)
        {
            ////.amazecats list
            ////.amazecats list adventures
            ///
            var message = string.Empty;

            switch (commandParts[0])
            {
                case Commands.ListAdventures:
                    message += "create - args here\n";
                    break;

                default:
                    message = "\n**Lists**\n";
                    message += "adventures - Available adventures\n";
                    message += "jobs - Available jobs\n";
                    break;
            }

            return new ResultMessage(message);
        }

        private async Task<ResultMessage> HandleCreate(string ownerId, IList<string> commandParts)
        {
            ////.amazecats create Name Male Energetic

            var name = commandParts[0].Trim();

            // Check if the cat already exists
            var existingCat = await _repository.FindByNameAsync(name);
            if (existingCat != null)
            {
                return new ResultMessage($"Error: A cat with the name '{name}' already exists");
            }

            Gender gender;
            if (!Enum.TryParse(commandParts[1].Trim(), out gender) || gender == Gender.None)
            {
                return new ResultMessage($"Error: Valid genders are '{Gender.Male}' and '{Gender.Female}'");
            }

            Personality personality;
            if (!Enum.TryParse(commandParts[2].Trim(), out personality) || personality == Personality.None)
            {
                return new ResultMessage($"Error: Valid genders are {Personality.Energetic} {Personality.Lazy}");
            }

            // Generate the new cat
            var newCat = new Cat(ownerId, name, gender, personality);

            // Store the new cat
            await _repository.AddAsync(newCat);
            await _repository.SaveChangesAsync();

            var catStatus = CatStatusEmbed(newCat);
            var result = $"{newCat.Name} happily meets {newCat.Addressing} new master\n";

            return new ResultMessage(result, catStatus);
        }

        private async Task<ResultMessage> HandleFeed(Cat cat, IList<string> commandParts)
        {
            ////.amazecats feed Meat

            throw new NotImplementedException();
        }

        private async Task<ResultMessage> HandlePoke(Cat cat)
        {
            ////.amazecats poke

            var result = $"{cat.Name} flops over\n";

            return new ResultMessage(result);
        }

        private ResultMessage HandleHelp(string commandPart)
        {
            ////.amazecats help
            ////.amazecats help create

            var message = "\n**Commands**\n";

            switch (commandPart)
            {
                case Commands.HelpCreate:
                    message += "create - todo: add args here\n";
                    break;

                default:
                    message += "poke - Pokes your cat\n";
                    message += "help - Shows this help\n";
                    ////message += "feed - Feeds your cat\n";
                    break;
            }

            return new ResultMessage(message);
        }

        private static Embed CatStatusEmbed(Cat cat)
        {
            var embedBuilder = new EmbedBuilder();

            var field1 = new EmbedFieldBuilder
            {
                IsInline = true,
                Name = "Age",
                Value = (int)((DateTimeOffset.UtcNow - cat.DateOfBirth).TotalDays / 7) + " weeks"
            };

            var field2 = new EmbedFieldBuilder
            {
                IsInline = true,
                Name = "Hunger",
                Value = cat.Stats.GetHungerLevel()
            };

            embedBuilder.Author = new EmbedAuthorBuilder() { Name = cat.Name };
            embedBuilder.AddField(field1);
            embedBuilder.AddField(field2);

            return embedBuilder.Build();
        }
    }
}