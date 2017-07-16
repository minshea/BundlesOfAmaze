using System;

namespace BundlesOfAmaze.Data
{
    public class Item : Entity, IEquatable<Item>
    {
        public ItemType ItemType { get; private set; }
        public string Name { get; private set; }
        public string Label { get; private set; }
        public string Description { get; private set; }

        protected Item()
        {
        }

        public Item(ItemType itemType, string name, string label, string description)
            : this()
        {
            ItemType = itemType;
            Name = name.ToLowerInvariant();
            Label = label;
            Description = description;
        }

        public virtual void Update(Item sourceItem)
        {
            ItemType = sourceItem.ItemType;
            Label = sourceItem.Label;
            Description = sourceItem.Description;
        }

        #region IEquatable implementation

        public bool Equals(Item other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ItemType == other.ItemType && string.Equals(Label, other.Label) && string.Equals(Description, other.Description);
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
                hashCode = (hashCode * 397) ^ (Label != null ? Label.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion IEquatable implementation
    }
}