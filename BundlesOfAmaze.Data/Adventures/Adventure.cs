using System;
using System.Collections.Generic;
using System.Linq;

namespace BundlesOfAmaze.Data
{
    public abstract class Adventure
    {
        public AdventureRef AdventureRef { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public TimeSpan Duration { get; private set; }

        public List<DropPoolItem> DropPool { get; private set; }

        public CatStats StatGain { get; protected set; }

        protected Adventure(AdventureRef adventureRef, string name, string description, TimeSpan duration)
        {
            AdventureRef = adventureRef;
            Name = name;
            Description = description;
            Duration = duration;
            DropPool = new List<DropPoolItem>();
        }

        public DropPoolItem GetReward()
        {
            // Prepare the drop table
            var dropItems = new List<DropItem>();
            var currentOffset = 0;

            foreach (var dropPoolItem in DropPool)
            {
                var start = currentOffset;
                var end = currentOffset + dropPoolItem.Weight;
                currentOffset += dropPoolItem.Weight + 1;

                dropItems.Add(new DropItem(dropPoolItem, start, end));
            }

            var random = new Random();

            // Roll and get the item
            var roll = random.Next(0, currentOffset - 1);
            var reward = dropItems.First(i => roll >= i.Start && roll <= i.End);

            return reward.DropPoolItem;
        }
    }
}