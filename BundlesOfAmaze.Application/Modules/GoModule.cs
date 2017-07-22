using System;
using System.Threading.Tasks;
using BundlesOfAmaze.Data;
using BundlesOfAmaze.Shared;
using Discord.Commands;

namespace BundlesOfAmaze.Application
{
    [Name("Go")]
    public class GoModule : ModuleBase
    {
        private readonly ICurrentOwner _currentOwner;
        private readonly IAdventureEntryRepository _adventureEntryRepository;
        private readonly ICatRepository _catRepository;
        private readonly ITelemetryService _telemetryService;

        public GoModule(ICurrentOwner currentOwner, IAdventureEntryRepository adventureEntryRepository,
            ICatRepository catRepository, ITelemetryService telemetryService)
        {
            _currentOwner = currentOwner;
            _adventureEntryRepository = adventureEntryRepository;
            _catRepository = catRepository;
            _telemetryService = telemetryService;
        }

        [Command("go")]
        [Summary("Goes somewhere. Use 'help go' for more information")]
        [Remarks("Usage: go [destination name]\nCommand to send your cat off on an adventure")]
        public async Task HandleAsync(string rawDestinationName)
        {
            if (string.IsNullOrWhiteSpace(rawDestinationName))
            {
                await ReplyAsync(Messages.InvalidCommand);
                return;
            }

            var cat = await _catRepository.FindByOwnerAsync(_currentOwner.Owner.Id);
            if (cat == null)
            {
                await ReplyAsync(Messages.CatNotOwned);
                return;
            }

            var existingAdventureEntry = await _adventureEntryRepository.FindByCatIdAsync(cat.Id);
            if (existingAdventureEntry != null)
            {
                await ReplyAsync(Messages.AdventureNotNull);
                return;
            }

            var destinationName = rawDestinationName.Trim().ToLowerInvariant();
            switch (destinationName)
            {
                case "explore-neighbourhood":
                    var adventure = new AdventureExploreNeighbourhood();

                    await RegisterAdventureAsync(cat, adventure);
                    Console.WriteLine($"Adding adventure {adventure.AdventureRef} for cat {cat.Id}");

                    // Track event
                    _telemetryService.TrackGoCommand(_currentOwner.Owner, cat, adventure);

                    await ReplyAsync($"{cat.Name} embarks on {adventure.Name}. {cat.Pronoun} will return in {adventure.Duration.TotalMinutes} minutes.");
                    return;

                default:
                    await ReplyAsync($"There is no adventure with the name '{destinationName}'");
                    return;
            }
        }

        private async Task RegisterAdventureAsync(Cat cat, Adventure adventure)
        {
            var start = DateTimeOffset.UtcNow;
            var end = start.Add(adventure.Duration);

            var adventureEntry = new AdventureEntry(adventure.AdventureRef, cat.Id, start, end);

            _adventureEntryRepository.Add(adventureEntry);
            await _adventureEntryRepository.SaveChangesAsync();
        }
    }
}