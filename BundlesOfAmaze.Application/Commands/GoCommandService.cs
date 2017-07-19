using System;
using System.Threading.Tasks;
using BundlesOfAmaze.Data;
using BundlesOfAmaze.Shared;

namespace BundlesOfAmaze.Application
{
    public class GoCommandService : IGoCommandService
    {
        private readonly IAdventureEntryRepository _adventureEntryRepository;
        private readonly ICatRepository _catRepository;

        public GoCommandService(IAdventureEntryRepository adventureEntryRepository, ICatRepository catRepository)
        {
            _adventureEntryRepository = adventureEntryRepository;
            _catRepository = catRepository;
        }

        public async Task<ResultMessage> HandleAsync(Owner owner, string rawDestinationName)
        {
            if (string.IsNullOrWhiteSpace(rawDestinationName))
            {
                return new ResultMessage(Messages.InvalidCommand);
            }

            var cat = await _catRepository.FindByOwnerAsync(owner.Id);
            if (cat == null)
            {
                return new ResultMessage(Messages.CatNotOwned);
            }

            var existingAdventureEntry = await _adventureEntryRepository.FindByCatIdAsync(cat.Id);
            if (existingAdventureEntry != null)
            {
                return new ResultMessage(Messages.AdventureNotNull);
            }

            var destinationName = rawDestinationName.Trim().ToLowerInvariant();
            switch (destinationName)
            {
                case "explore-neighbourhood":
                    var adventure = new AdventureExploreNeighbourhood();
                    await RegisterAdventureAsync(cat, adventure);

                    Console.WriteLine($"Adding adventure {adventure.AdventureRef} for cat {cat.Id}");
                    return new ResultMessage($"{cat.Name} embarks on {adventure.Name}. {cat.Pronoun} will return in {adventure.Duration.TotalMinutes} minutes.");

                default:
                    return new ResultMessage($"There is no adventure with the name '{destinationName}'");
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