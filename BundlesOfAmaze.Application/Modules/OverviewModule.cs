using System.Threading.Tasks;
using BundlesOfAmaze.Data;
using Discord.Commands;

namespace BundlesOfAmaze.Application
{
    [Name("Overview")]
    public class OverviewModule : ModuleBase
    {
        private readonly ICurrentOwner _currentOwner;
        private readonly ICatRepository _catRepository;

        public OverviewModule(ICurrentOwner currentOwner, ICatRepository catRepository)
        {
            _currentOwner = currentOwner;
            _catRepository = catRepository;
        }

        [Command("overview")]
        public async Task HandleAsync()
        {
            var errorMessage = "You don't have a cat yet!\nCreate one using the 'create' command.";

            if (_currentOwner.Owner == null)
            {
                await ReplyAsync(errorMessage);
                return;
            }

            var cat = await _catRepository.FindByOwnerAsync(_currentOwner.Owner.Id);
            if (cat == null)
            {
                await ReplyAsync(errorMessage);
            }

            await ReplyAsync(string.Empty, embed: CatSheet.GetSheet(cat));
        }
    }
}