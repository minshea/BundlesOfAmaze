using System.Collections.ObjectModel;

namespace BundlesOfAmaze.Data
{
    public class Owner : Entity
    {
        public string AuthorId { get; private set; }

        public string Name { get; private set; }

        ////public Cat Cat { get; private set; }

        public Collection<OwnerInventoryItem> InventoryItems { get; private set; }

        protected Owner()
        {
            InventoryItems = new Collection<OwnerInventoryItem>();
        }

        public Owner(string authorId, string name)
            : this()
        {
            AuthorId = authorId;
            Name = name;
            ////Cat = cat;

            ////InventoryItems.Add(new OwnerInventoryItem());
        }
    }
}