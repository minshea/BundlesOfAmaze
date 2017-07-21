using System.Threading.Tasks;
using BundlesOfAmaze.Data;
using BundlesOfAmaze.Shared;
using Discord.Commands;

namespace BundlesOfAmaze.Application
{
    [Name("Give")]
    public class GiveModule : ModuleBase
    {
        private readonly ICurrentOwner _currentOwner;
        private readonly ICatRepository _catRepository;
        private readonly IItemRepository _itemRepository;

        public GiveModule(ICurrentOwner currentOwner, ICatRepository catRepository, IItemRepository itemRepository)
        {
            _currentOwner = currentOwner;
            _catRepository = catRepository;
            _itemRepository = itemRepository;
        }

        [Command("give"), Alias("g")]
        [Summary("Gives an item. Use 'help give' for more information")]
        [Remarks("Usage: give [item name]\nCommand to feed or give an item to your cat\nex. give tuna\n")]
        public async Task HandleAsync(string itemName)
        {
            ////.amazecats give itemname

            var item = await _itemRepository.FindByNameAsync(itemName);
            if (item == null)
            {
                await ReplyAsync($"An item with the name '{itemName}' does not exist, sorry");
                return;
            }

            var inventoryItem = _currentOwner.Owner.GetItem(item.ItemRef);
            if (inventoryItem == null)
            {
                await ReplyAsync($"You do not have any {item.Name}");
                return;
            }

            var cat = await _catRepository.FindByOwnerAsync(_currentOwner.Owner.Id);
            if (cat == null)
            {
                await ReplyAsync(Messages.CatNotOwned);
                return;
            }

            var result = inventoryItem.DecreaseQuantity(1);
            if (!result)
            {
                await ReplyAsync($"You do not have enough {item.Name}");
                return;
            }

            cat.GiveItem(item, 1);

            await _catRepository.SaveChangesAsync();

            var message = string.Empty;
            switch (item.ItemType)
            {
                case ItemType.Food:
                    message = $"{cat.Name} happily noms on the {item.Name}";
                    break;

                case ItemType.Drink:
                    message = $"{cat.Name} happily drinks the {item.Name}";
                    break;

                case ItemType.Currency:
                case ItemType.Consumable:
                case ItemType.Equipment:
                    message = $"{cat.Name} takes the {item.Name}";
                    break;
            }

            await ReplyAsync(message, embed: CatSheet.GetSheet(cat));
        }
    }
}