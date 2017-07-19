using System;

namespace BundlesOfAmaze.Data
{
    public class Item : Entity, IEquatable<Item>
    {
        public ItemType ItemType { get; private set; }
        public ItemRef ItemRef { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        protected Item()
        {
        }

        public Item(ItemType itemType, ItemRef itemRef, string name, string description)
            : this()
        {
            ItemType = itemType;
            ItemRef = itemRef;
            Name = name;
            Description = description;
        }

        public virtual void Update(Item sourceItem)
        {
            ItemType = sourceItem.ItemType;
            Name = sourceItem.Name;
            Description = sourceItem.Description;
        }

        #region IEquatable implementation

        public bool Equals(Item other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ItemType == other.ItemType && ItemRef == other.ItemRef && string.Equals(Name, other.Name) && string.Equals(Description, other.Description);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Item)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int)ItemType;
                hashCode = (hashCode * 397) ^ (int)ItemRef;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion IEquatable implementation
    }
}