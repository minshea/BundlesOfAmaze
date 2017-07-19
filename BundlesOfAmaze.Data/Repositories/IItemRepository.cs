using System.Threading.Tasks;

namespace BundlesOfAmaze.Data
{
    public interface IItemRepository : IRepository<Item>
    {
        Task<Item> FindByNameAsync(string itemName);

        Task<Item> FindByItemRefAsync(ItemRef rewardItemRef);
    }
}