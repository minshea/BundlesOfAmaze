namespace BundlesOfAmaze.Data
{
    public class OwnerInventoryItem : Entity
    {
        #region EF Mapping

        public long OwnerId { get; private set; }

        public virtual Owner Owner { get; private set; }

        #endregion EF Mapping

        public ItemRef ItemRef { get; private set; }

        public int Quantity { get; private set; }

        protected OwnerInventoryItem()
        {
        }

        public OwnerInventoryItem(ItemRef itemRef, int quantity)
            : this()
        {
            ItemRef = itemRef;
            Quantity = quantity;
        }

        public bool DecreaseQuantity(int quantity)
        {
            if (Quantity - quantity < 0)
            {
                return false;
            }

            Quantity -= quantity;
            return true;
        }

        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }
    }
}