using System.Threading.Tasks;
using BundlesOfAmaze.Data;
using Discord;

namespace BundlesOfAmaze.Application
{
    public class OverviewService : IOverviewService
    {
        private readonly IAdventureRepository _adventureRepository;
        private readonly IAdventureEntryRepository _adventureEntryRepository;

        public OverviewService(IAdventureRepository adventureRepository, IAdventureEntryRepository adventureEntryRepository)
        {
            _adventureRepository = adventureRepository;
            _adventureEntryRepository = adventureEntryRepository;
        }

        public async Task<ResultMessage> GetOverviewAsync(Cat cat)
        {
            Embed embed;

            var adventureEntry = await _adventureEntryRepository.FindByCatIdAsync(cat.Id);
            if (adventureEntry != null)
            {
                var adventure = _adventureRepository.FindByAdventureRef(adventureEntry.AdventureRef);

                embed = CatSheet.GetSheet(cat, adventure.Name, adventureEntry.End);
            }
            else
            {
                embed = CatSheet.GetSheet(cat);
            }

            return new ResultMessage(string.Empty, embed);
        }
    }
}