using System.Linq;
using System.Threading.Tasks;
using BundlesOfAmaze.Data;
using Discord;
using Discord.Commands;

namespace BundlesOfAmaze.Application
{
    [Name("List")]
    public class ListModule : ModuleBase
    {
        private readonly ICurrentOwner _currentOwner;
        private readonly IItemRepository _itemRepository;

        public ListModule(ICurrentOwner currentOwner, IItemRepository itemRepository)
        {
            _currentOwner = currentOwner;
            _itemRepository = itemRepository;
        }

        [Command("list"), Alias("l")]
        [Summary("Lists possible activities. Use 'help list' for more information")]
        [Remarks("Usage: list")]
        public async Task HandleAsync()
        {
            ////.amazecats list

            var message = "\n**Lists**\n";
            message += "inventory - Your inventory\n";
            message += "adventures - Available adventures\n";
            message += "jobs - Available jobs\n";

            await ReplyAsync(message);
        }

        [Command("list"), Alias("l")]
        [Summary("Lists possible activities. Use 'help list' for more information")]
        [Remarks("Usage: list [name]\nCommand to list activities or adventures your cat can embark on")]
        public async Task HandleAsync(string list)
        {
            ////.amazecats list adventures

            var message = string.Empty;
            var embed = default(Embed);

            switch (list)
            {
                case Commands.ListAdventures:
                    message = "**Available adventures**\n\n";
                    message += "Usage: .amazecats go [adventure name]\n\n";
                    message += "- explore-neighbourhood\n";
                    break;

                case Commands.ListActivities:
                    message += "TODO: list of available activities\n";
                    break;

                case Commands.ListInventory:

                    var itemRefs = _currentOwner.Owner.InventoryItems.Select(i => i.ItemRef).ToList();
                    var items = await _itemRepository.FindAllMatchingAsync(i => itemRefs.Contains(i.ItemRef));

                    var embedBuilder = new EmbedBuilder();
                    foreach (var item in items.OrderBy(i => i.Name))
                    {
                        var quantity = _currentOwner.Owner.InventoryItems.Single(i => i.ItemRef == item.ItemRef).Quantity;
                        var amount = quantity > 0 ? quantity.ToString() : "none";

                        var itemField = new EmbedFieldBuilder
                        {
                            IsInline = false,
                            Name = item.Name,
                            Value = $"{item.Description}\nAmount: {amount}"
                        };
                        embedBuilder.AddField(itemField);
                    }

                    message = "\n**Inventory**\n";
                    embed = embedBuilder.Build();
                    break;

                default:
                    message = "\n**Lists**\n";
                    message += "inventory - Your inventory\n";
                    message += "adventures - Available adventures\n";
                    message += "jobs - Available jobs\n";
                    break;
            }

            await ReplyAsync(message, embed: embed);
        }
    }
}