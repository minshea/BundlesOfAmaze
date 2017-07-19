namespace BundlesOfAmaze.Data
{
    public class DropPoolItem
    {
        public ItemRef ItemRef { get; private set; }

        public int Quantity { get; private set; }

        public int Weight { get; private set; }

        public DropPoolItem(ItemRef itemRef, int quantity, int weight)
        {
            ItemRef = itemRef;
            Quantity = quantity;
            Weight = weight;
        }
    }
}