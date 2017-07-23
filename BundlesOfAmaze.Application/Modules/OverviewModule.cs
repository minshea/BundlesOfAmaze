using System.Threading.Tasks;
using BundlesOfAmaze.Shared;
using Discord.Commands;

namespace BundlesOfAmaze.Application
{
    [Name("Overview")]
    public class OverviewModule : ModuleBase
    {
        private readonly ICurrentOwner _currentOwner;
        private readonly IOverviewService _overviewService;
        private readonly ITelemetryService _telemetryService;

        public OverviewModule(ICurrentOwner currentOwner, IOverviewService overviewService, ITelemetryService telemetryService)
        {
            _currentOwner = currentOwner;
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

            if (_currentOwner.Cat == null)
            {
                await ReplyAsync(Messages.CatNotOwned);
                return;
            }

            // Track event
            _telemetryService.TrackOverviewCommand(_currentOwner, _currentOwner.Cat);

            var overview = await _overviewService.GetOverviewAsync(_currentOwner, _currentOwner.Cat);
            await ReplyAsync(overview.Message, embed: overview.Embed);
        }
    }
}