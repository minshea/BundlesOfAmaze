namespace BundlesOfAmaze.Data
{
    public class OwnerInventoryItem : Entity
    {
        #region EF Mapping

        public long OwnerId { get; private set; }

        public virtual Owner Owner { get; private set; }

        #endregion EF Mapping

        public long ItemId { get; private set; }

        public int Quantity { get; private set; }

        protected OwnerInventoryItem()
        {
        }

        public OwnerInventoryItem(int itemId, int quantity)
            : this()
        {
            ItemId = itemId;
            Quantity = quantity;
        }
    }
}