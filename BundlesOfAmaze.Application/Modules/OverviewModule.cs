using System.Threading.Tasks;
using BundlesOfAmaze.Data;
using BundlesOfAmaze.Shared;
using Discord.Commands;

namespace BundlesOfAmaze.Application
{
    [Name("Overview")]
    public class OverviewModule : ModuleBase
    {
        private readonly ICurrentOwner _currentOwner;
        private readonly ICatRepository _catRepository;
        private readonly IOverviewService _overviewService;
        private readonly ITelemetryService _telemetryService;

        public OverviewModule(ICurrentOwner currentOwner, ICatRepository catRepository, IOverviewService overviewService, ITelemetryService telemetryService)
        {
            _currentOwner = currentOwner;
            _catRepository = catRepository;
            _overviewService = overviewService;
            _telemetryService = telemetryService;
        }

        [Command("overview")]
        public async Task HandleAsync()
        {
            if (_currentOwner.Owner == null)
            {
                await ReplyAsync(Messages.CatNotOwned);
                return;
            }

            var cat = await _catRepository.FindByOwnerAsync(_currentOwner.Owner.Id);
            if (cat == null)
            {
                await ReplyAsync(Messages.CatNotOwned);
                return;
            }

            // Track event
            _telemetryService.TrackOverviewCommand(_currentOwner.Owner, cat);

            var overview = await _overviewService.GetOverviewAsync(cat);
            await ReplyAsync(overview.Message, embed: overview.Embed);
        }
    }
}