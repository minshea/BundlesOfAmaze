using System.Collections.Generic;
using System.Linq;

namespace BundlesOfAmaze.Data
{
    public class AdventureRepository : IAdventureRepository
    {
        private List<Adventure> _adventures;

        public AdventureRepository()
        {
            GenerateAdventures();
        }

        private void GenerateAdventures()
        {
            _adventures = new List<Adventure>
            {
                new AdventureExploreNeighbourhood()
            };
        }

        public Adventure FindByAdventureRef(AdventureRef adventureRef)
        {
            return _adventures.FirstOrDefault(i => i.AdventureRef == adventureRef);
        }
    }
}