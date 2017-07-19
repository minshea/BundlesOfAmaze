using System;

namespace BundlesOfAmaze.Data
{
    public class AdventureEntry : Entity
    {
        public AdventureRef AdventureRef { get; private set; }

        public long CatId { get; private set; }

        public DateTimeOffset Start { get; private set; }

        public DateTimeOffset End { get; private set; }

        protected AdventureEntry()
        {
        }

        public AdventureEntry(AdventureRef adventureRef, long catId, DateTimeOffset start, DateTimeOffset end)
            : this()
        {
            AdventureRef = adventureRef;
            CatId = catId;
            Start = start;
            End = end;
        }
    }
}