using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BundlesOfAmaze.Data;
using Discord;

namespace BundlesOfAmaze.Application
{
    public class ListCommandService : IListCommandService
    {
        private readonly IItemRepository _itemRepository;

        public ListCommandService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<ResultMessage> HandleAsync(Owner owner, string commandPart)
        {
            ////.amazecats list
            ////.amazecats list adventures

            var message = string.Empty;
            var embed = default(Embed);

            switch (commandPart)
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

                    var itemRefs = owner.InventoryItems.Select(i => i.ItemRef).ToList();
                    var items = await _itemRepository.FindAllMatchingAsync(i => itemRefs.Contains(i.ItemRef));

                    var embedBuilder = new EmbedBuilder();
                    foreach (var item in items.OrderBy(i => i.Name))
                    {
                        var quantity = owner.InventoryItems.Single(i => i.ItemRef == item.ItemRef).Quantity;
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

            return new ResultMessage(message, embed);
        }
    }
}