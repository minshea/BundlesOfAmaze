using System;
using System.Collections.ObjectModel;
using System.Linq;
using Discord;

namespace BundlesOfAmaze.Data
{
    public class Owner : Entity
    {
        public string AuthorId { get; private set; }

        public string AvatarUrl { get; private set; }

        public string Name { get; private set; }

        public Collection<OwnerInventoryItem> InventoryItems { get; private set; }

        protected Owner()
        {
            InventoryItems = new Collection<OwnerInventoryItem>();
        }

        public Owner(IUser user)
            : this()
        {
            AuthorId = user.Id.ToString();
            Name = user.Username;
            AvatarUrl = user.GetAvatarUrl(ImageFormat.Auto, 64);
        }

        public void Update(IUser user)
        {
            if (AuthorId != user.Id.ToString())
            {
                throw new ArgumentException($"The given user {user.Id} does not match the one that is updated {AuthorId}!");
            }

            Name = user.Username;
            AvatarUrl = user.GetAvatarUrl(ImageFormat.Auto, 64);
        }

        public OwnerInventoryItem GetItem(ItemRef itemRef)
        {
            return InventoryItems.FirstOrDefault(i => i.ItemRef == itemRef);
        }

        public void GiveItem(ItemRef itemRef, int quantity)
        {
            var existing = InventoryItems.FirstOrDefault(i => i.ItemRef == itemRef);
            if (existing == null)
            {
                InventoryItems.Add(new OwnerInventoryItem(itemRef, quantity));
            }
            else
            {
                existing.AddQuantity(quantity);
            }
        }

        public bool FetchItem(ItemRef itemRef, int amount)
        {
            var inventoryItem = InventoryItems.FirstOrDefault(i => i.ItemRef == itemRef);
            if (inventoryItem == null || inventoryItem.Quantity < amount)
            {
                return false;
            }

            inventoryItem.DecreaseQuantity(amount);
            if (inventoryItem.Quantity == 0)
            {
                InventoryItems.Remove(inventoryItem);
            }

            return true;
        }
    }
}