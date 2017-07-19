using System.Threading.Tasks;
using BundlesOfAmaze.Data;
using BundlesOfAmaze.Shared;

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

            var inventoryItem = owner.GetItem(item.ItemRef);
            if (inventoryItem == null)
            {
                return new ResultMessage($"You do not have any {item.Name}");
            }

            var cat = await _catRepository.FindByOwnerAsync(owner.Id);
            if (cat == null)
            {
                return new ResultMessage(Messages.CatNotOwned);
            }

            var result = inventoryItem.DecreaseQuantity(1);
            if (!result)
            {
                return new ResultMessage($"You do not have enough {item.Name}");
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

            return new ResultMessage(message, CatSheet.GetSheet(cat));
        }
    }
}