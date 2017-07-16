using System;
using System.Threading.Tasks;
using BundlesOfAmaze.Data;

namespace BundlesOfAmaze.Application
{
    public class GiveCommandService : IGiveCommandService
    {
        private readonly ICatRepository _catRepository;
        private readonly IItemRepository _itemRepository;

        public GiveCommandService(ICatRepository catRepository, IItemRepository itemRepository)
        {
            _catRepository = catRepository;
            _itemRepository = itemRepository;
        }

        public async Task<ResultMessage> HandleAsync(Owner owner, string itemName)
        {
            ////.amazecats give itemname

            var item = await _itemRepository.FindByNameAsync(itemName);
            if (item == null)
            {
                return new ResultMessage($"An item with the name '{itemName}' does not exist, sorry");
            }

            var inventoryItem = owner.GetItem(item.Id);
            if (inventoryItem == null)
            {
                return new ResultMessage($"You do not have any {item.Label}");
            }

            var cat = await _catRepository.FindByOwnerAsync(owner.Id);
            if (cat == null)
            {
                return new ResultMessage("You do not own a cat");
            }

            if (inventoryItem.Quantity < 1)
            {
                return new ResultMessage($"You do not have enough {item.Label}");
            }

            inventoryItem.DecreaseQuantity(1);
            cat.GiveItem(item, 1);

            await _catRepository.SaveChangesAsync();

            var message = string.Empty;
            switch (item.ItemType)
            {
                case ItemType.Food:
                    message = $"{cat.Name} happily noms on the {item.Label}";
                    break;

                case ItemType.Drink:
                    message = $"{cat.Name} happily drinks the {item.Label}";
                    break;

                case ItemType.Currency:
                case ItemType.Consumable:
                case ItemType.Equipment:
                    message = $"{cat.Name} takes the {item.Label}";
                    break;
            }

            return new ResultMessage(message, CatSheet.GetSheet(cat));
        }
    }
}