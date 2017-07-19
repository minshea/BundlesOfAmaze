using System;

namespace BundlesOfAmaze.Data
{
    public class FoodItem : Item, IEquatable<FoodItem>
    {
        public int FoodValue { get; private set; }

        protected FoodItem() : base()
        {
        }

        public FoodItem(ItemType itemType, ItemRef itemRef, string name, string description, int foodValue)
            : base(itemType, itemRef, name, description)
        {
            FoodValue = foodValue;
        }

        #region IEquatable implementation

        public bool Equals(FoodItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && FoodValue == other.FoodValue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FoodItem)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ FoodValue;
            }
        }

        #endregion IEquatable implementation
    }
}