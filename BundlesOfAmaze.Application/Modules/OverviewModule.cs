using System.Linq;
using System.Threading.Tasks;
using BundlesOfAmaze.Data;
using BundlesOfAmaze.Shared;
using Discord;
using Discord.Commands;

namespace BundlesOfAmaze.Application
{
    [Name("Overview")]
    public class OverviewModule : ModuleBase
    {
        private readonly ICurrentOwner _currentOwner;
        private readonly IOverviewService _overviewService;
        private readonly ITelemetryService _telemetryService;
        private readonly IItemRepository _itemRepository;

        public OverviewModule(ICurrentOwner currentOwner, IOverviewService overviewService, ITelemetryService telemetryService, IItemRepository itemRepository)
        {
            _currentOwner = currentOwner;
            _overviewService = overviewService;
            _telemetryService = telemetryService;
            _itemRepository = itemRepository;
        }

        [Command("overview")]
        public async Task HandleOverviewAsync()
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

        [Command("inventory")]
        [Summary("Shows your current inventory")]
        public async Task HandleInventoryAsync()
        {
            var itemRefs = _currentOwner.Owner.InventoryItems.Select(i => i.ItemRef).ToList();
            var items = await _itemRepository.FindAllMatchingAsync(i => itemRefs.Contains(i.ItemRef));

            var embedBuilder = new EmbedBuilder
            {
                Color = new Color(226, 193, 5),
                Author = new EmbedAuthorBuilder
                {
                    Name = $"{_currentOwner.Owner.Name}'s inventory",
                    IconUrl = _currentOwner.Owner.AvatarUrl
                }
            };

            foreach (var item in items.OrderBy(i => i.Name))
            {
                var quantity = _currentOwner.Owner.InventoryItems.Single(i => i.ItemRef == item.ItemRef).Quantity;

                var itemField = new EmbedFieldBuilder
                {
                    IsInline = true,
                    Name = $"{quantity}x {item.Name}",
                    Value = item.Description
                };
                embedBuilder.AddField(itemField);
            }

            await ReplyAsync(string.Empty, embed: embedBuilder.Build());
        }
    }
}