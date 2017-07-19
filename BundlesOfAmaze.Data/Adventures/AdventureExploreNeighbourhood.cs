using System;

namespace BundlesOfAmaze.Data
{
    public class AdventureExploreNeighbourhood : Adventure
    {
        public AdventureExploreNeighbourhood()
            : base(AdventureRef.ExploreNeighbourhood, "Explore neighbourhood", "Explore neighbourhood description", TimeSpan.FromMinutes(10))
        {
            DropPool.Add(new DropPoolItem(ItemRef.Credit, 10, 15));
            DropPool.Add(new DropPoolItem(ItemRef.Water, 1, 25));
            DropPool.Add(new DropPoolItem(ItemRef.Milk, 1, 10));
            DropPool.Add(new DropPoolItem(ItemRef.Tuna, 1, 25));
            DropPool.Add(new DropPoolItem(ItemRef.Goldfish, 1, 1));

            StatGain = new CatStats
            {
                Hunger = -2000,
                Thirst = -1000,
                Kind = 0,
                Lazy = -2,
                Resourceful = 2,
                Outgoing = 5,
                High = -10
            };
        }
    }
}