namespace BundlesOfAmaze.Data
{
    public class DropItem
    {
        public DropPoolItem DropPoolItem { get; private set; }

        public int Start { get; private set; }

        public int End { get; private set; }

        public DropItem(DropPoolItem dropPoolItem, int start, int end)
        {
            DropPoolItem = dropPoolItem;
            Start = start;
            End = end;
        }
    }
}